using SportGames.Sourcing.SourceEngine;

namespace SportGames.GamesProcessing;

public class GamesQueueHandler
{
    private readonly IGamesProcessor _gamesProcessor;

    public GamesQueueHandler(IGamesProcessor gamesProcessor)
    {
        _gamesProcessor = gamesProcessor;
    }

    public void Read()
    {
        // Just a dummy code to display the intention:
        // 1 here we are listening to the distributed Queue with parsed SourcedGameInfo objects
        // 2 queueHandler then tries to save the games into persistent DB (the same which is used by API)
        while (true)
        {
            // reading the queue where the parsed GameInfo objects are waiting for processing
            var gameInfo = new SourcedGameInfo();
            _gamesProcessor.Handle(gameInfo);
        }
    }
}