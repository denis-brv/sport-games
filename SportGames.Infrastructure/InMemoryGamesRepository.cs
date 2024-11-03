using System.Collections.Concurrent;

namespace SportGames.Infrastructure;

/*
 * Used for demo and test purposes.
 * In real application a DB-related implementation (e.g. based on Entity Framework) should be used.
 */

public class InMemoryGamesRepository : IGamesRepository
{
    public readonly ConcurrentBag<Game> Games = new();

    public async ValueTask<bool> SaveGame(Game game)
    {
        Games.Add(game);
        return await ValueTask.FromResult(true);
    }

    public async Task<IEnumerable<Game>> GetGames()
    {
        return await Task.FromResult(Games);
    }
}