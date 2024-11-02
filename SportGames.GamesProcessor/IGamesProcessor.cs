using SportGames.Sourcing.SourceEngine;

namespace SportGames.GamesProcessing;

public interface IGamesProcessor
{
    void Handle(SourcedGameInfo gameInfo);
}