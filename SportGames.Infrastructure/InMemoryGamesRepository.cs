using System.Collections.Concurrent;

namespace SportGames.Infrastructure;

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

    public async Task<IEnumerable<Game>> FindGames(SearchGamesFilter filter)
    {
        var games = Games
            .Where(x => x.SportType == filter.SportType
                        && x.Competition == filter.Competition
                        && x.DateTime >= filter.MinGameDateTimeToSearch)
            .Where(x =>
                x.Teams[0] == filter.Team1 && x.Teams[1] == filter.Team2 ||
                x.Teams[0] == filter.Team2 && x.Teams[1] == filter.Team1);

        return await Task.FromResult(games.ToList());
    }
}