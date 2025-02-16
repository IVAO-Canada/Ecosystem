using Microsoft.EntityFrameworkCore;

using Website.Data.Discord;

namespace Website.Data;

public class Context : DbContext
{
	public DbSet<Category> DiscordCategories { get; set; }
	public DbSet<Channel> DiscordChannels { get; set; }
	public DbSet<Role> DiscordRoles { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={nameof(Context)}.db");
}
