using SportGames.Sourcing.SourceEngine;

namespace SportGames.Sourcing;

// Here analysis of many sources is done and the found reasonable information is put into a distributed queue
public class SourcesAnalyzer
{
    private readonly IGamesQueue _distributedGamesQueue;
    private readonly List<ISourceAnalyzer> _analyzers;
    public SourcesAnalyzer(IGamesQueue gamesQueue, List<ISourceAnalyzer> analyzers)
    {
        _distributedGamesQueue = gamesQueue;
        _analyzers = analyzers;
    }
    public void Run()
    {
        // this part should be done in parallel style, but simplified just for demo purposes
        foreach (var analyzer in _analyzers)
        {
            // ...read sources and get the results
            // enqueue the parsed Game info to a distributed queue
            analyzer.Analyze();
            _distributedGamesQueue.Enqueue(new SourcedGameInfo());
        }
    }
}