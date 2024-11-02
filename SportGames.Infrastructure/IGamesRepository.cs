namespace SportGames.Infrastructure;

public interface IGamesRepository
{
    ValueTask<bool> SaveGame(Game game);
    Task<IEnumerable<Game>> GetGames();
}
