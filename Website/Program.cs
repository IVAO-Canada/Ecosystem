using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;

using Website;
using Website.Data;
using Website.Services;

if (!File.Exists("deployment.json"))
{
    Console.Error.WriteLine("No deployment.json file found. Exiting...");
    Environment.Exit(-1);
}

string deploymentFileContents = File.ReadAllText("deployment.json");

if (JsonSerializer.Deserialize<Deployment>(deploymentFileContents) is not Deployment deployment)
{
	Console.Error.WriteLine("Invalid deployment.json file found. Exiting...");
	Environment.Exit(-2);
    return;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication("ivao_scheme")
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie("cookie")
    .AddOpenIdConnect("ivao_scheme", options =>
    {
        options.SignInScheme = "cookie";
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.MapInboundClaims = false;
        options.TokenValidationParameters = new() {
            NameClaimType = "sub"
        };

        options.Authority = "https://api.ivao.aero";
        options.ResponseType = Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectResponseType.Code;
        options.UsePkce = true;
        options.Scope.Add("profile");

        options.ClientId = deployment.OidcId;
        options.ClientSecret = deployment.OidcSecret;
    });

builder.Services.AddAuthorization().AddCascadingAuthenticationState();

builder.Services.AddDbContextFactory<Context>(options => options.UseSqlite($"Data Source={nameof(Context)}.db"));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton(deployment);
builder.Services.AddSingleton<DiscordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGroup("/auth").MapGet("/login", (string? returnUrl) => TypedResults.Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" })).AllowAnonymous();

await app.Services.GetRequiredService<DiscordService>().RunAsync();

app.Run();
