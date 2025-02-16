namespace Website.Data.Events;

public class Event
{
	public int EventId { get; set; }
	public required string Name { get; set; }
	public required string Location { get; set; }
	public required DateTimeOffset Start { get; set; }
	public required DateTimeOffset End { get; set; }
}