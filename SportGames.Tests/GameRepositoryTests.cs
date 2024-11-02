using FluentAssertions;
using SportGames.Infrastructure;

namespace SportGames.Tests;

public class GameRepositoryTests
{
    private readonly GameService _gameService;
    private readonly InMemoryGamesRepository _repository;
    
    public GameRepositoryTests()
    {
        _repository = new InMemoryGamesRepository();
        _gameService = new GameService(_repository);
    }
    
    
    [Fact]
    public async Task ShouldNotSaveDuplicateGames_WhenSameTeams()
    {
        // Assert
        var date = DateTime.UtcNow;
        var game = new Game
        {
            SportType = SportType.Football,
            DateTime = date,
            Competition = "EPL",
            Teams = new[] { "Arsenal", "Newcastle" }
        };
        _repository.Games.Add(game);
        
        // Act
        // try to add the same game
        await _gameService.SaveGame(game);
        
        // Assert
        _repository.Games.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task ShouldNotSaveDuplicateGames_WhenTeamsHasDifferentOrder()
    {
        // Assert
        var date = DateTime.UtcNow;
        var game1 = new Game
        {
            SportType = SportType.Football,
            DateTime = date,
            Competition = "EPL",
            Teams = new[] { "Arsenal", "Newcastle" }
        };
        var game2 = game1 with { Teams = new[] { "Newcastle", "Arsenal" } };
        
        // Act
        // try to add the same game
        await _gameService.SaveGame(game1);
        await _gameService.SaveGame(game2);
        
        // Assert
        _repository.Games.Count.Should().Be(1);
    }
    
    [Theory]
    [InlineData(1, false)]
    [InlineData(30, false)]
    [InlineData(120, false)]
    [InlineData(121, true)]
    public async Task ShouldNotSaveDuplicateGames_WhenWithinGameTimeFrame(int secondGameMinutesAhead, bool isSecondGameSaved)
    {
        // Assert
        var date = DateTime.UtcNow;
        var game1 = new Game
        {
            SportType = SportType.Football,
            DateTime = date,
            Competition = "EPL",
            Teams = new[] { "Arsenal", "Newcastle" }
        };
        var game2 = game1 with { DateTime = game1.DateTime.AddMinutes(secondGameMinutesAhead) };
        
        // Act
        // try to add the same game
        await _gameService.SaveGame(game1);
        await _gameService.SaveGame(game2);
        
        // Assert
        var gamesCount = _repository.Games.Count;
        (gamesCount == 2).Should().Be(isSecondGameSaved);
    }
}