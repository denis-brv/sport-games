namespace SportGames.Infrastructure;

public record SearchGamesFilter(
    SportType SportType,
    string Competition,
    string Team1,
    string Team2,
    DateTime? MinGameDateTimeToSearch = null);