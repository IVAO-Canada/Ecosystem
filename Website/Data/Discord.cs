using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Data.Discord;

[Table("discord-categories")]
public class Category
{
	[Key]
	public required string Name { get; set; }

	public ICollection<Channel> Channels { get; } = [];

	public string[] ReadRoles { get; set; } = [];
	public string[] WriteRoles { get; set; } = [];
	public string[] AdminRoles { get; set; } = [];
}

/// <summary>A Discord channel. Voice by default</summary>
/// <seealso cref="TextChannel"/>
[Table("discord-channels")]
[PrimaryKey(nameof(Name), nameof(CategoryName))]
public class Channel
{
	public required string Name { get; set; }

	public string[] ReadRoles { get; set; } = [];
	public string[] WriteRoles { get; set; } = [];
	public string[] AdminRoles { get; set; } = [];

	public required string CategoryName { get; set; }
	public required Category Category { get; set; }
}

public class TextChannel : Channel
{
	public string[] SeedMessages { get; set; } = [];
}

[Table("discord-roles")]
public class Role
{
	[Key]
	public required string Name { get; set; }

	public string? Colour { get; set; }
}