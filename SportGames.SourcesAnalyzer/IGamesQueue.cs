namespace SportGames.Sourcing.SourceEngine;

public interface IGamesQueue
{
    void Enqueue(SourcedGameInfo gameInfo);
}