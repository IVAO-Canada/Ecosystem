using Discord.WebSocket;

using Microsoft.EntityFrameworkCore;

using Website.Data;

namespace Website.Services;

public class DiscordService(IDbContextFactory<Context> _dbFactory, Deployment _deployment, ILogger<DiscordService> _logger) : IAsyncDisposable
{
	readonly DiscordSocketClient _client = new();

	public async Task RunAsync()
	{
		_client.Log += (msg) => { _logger.LogInformation("{msg}", msg.Message); return Task.CompletedTask; };

		await _client.LoginAsync(Discord.TokenType.Bot, _deployment.DiscordToken);
		await _client.StartAsync();
	}

	public async ValueTask DisposeAsync() => await _client.StopAsync();
}
