namespace SportGames.Infrastructure;

public class GameService
{
    private readonly IGamesRepository _gamesRepository;
    private readonly TimeSpan _gameUniquenessTimeFrame = TimeSpan.FromHours(2);
    private readonly TimeSpan _searchForGamesNotOlderThanDays = TimeSpan.FromDays(1);

    public GameService(IGamesRepository gamesRepository)
    {
        _gamesRepository = gamesRepository;
    }

    public async Task<IEnumerable<Game>> SearchGames(SearchGamesFilter filter)
    {
        var allGames = await _gamesRepository.GetGames();
        var games = allGames
            .Where(x => x.SportType == filter.SportType && x.Competition == filter.Competition)
            .Where(x =>
                x.Teams[0] == filter.Team1 && x.Teams[1] == filter.Team2 ||
                x.Teams[0] == filter.Team2 && x.Teams[1] == filter.Team1);

        if (filter.MinGameDateTimeToSearch.HasValue)
        {
            games = games.Where(x => x.DateTime >= filter.MinGameDateTimeToSearch);
        }

        return games;
    }

    public async ValueTask<bool> SaveGame(Game game)
    {
        if (!await ShouldSave(game))
        {
            return false;
        }

        return await _gamesRepository.SaveGame(game);
    }

    private async ValueTask<bool> ShouldSave(Game game)
    {
        var searchTimeLimitation = game.DateTime.Add(-_searchForGamesNotOlderThanDays);
        var searchFilter = new SearchGamesFilter(game.SportType, game.Competition, game.Teams[0], game.Teams[1],
            searchTimeLimitation);

        var gamesWithSameTeams = await SearchGames(searchFilter);
        var hasSameGame =
            gamesWithSameTeams.Any(x => x.DateTime >= game.DateTime.Add(-_gameUniquenessTimeFrame));

        return !hasSameGame;
    }
}