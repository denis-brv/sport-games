namespace SportGames.Infrastructure;

public record Game
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; init; }
    public SportType SportType { get; init; }
    public string Competition { get; init; }
    public string[] Teams { get; init; }
}