using SportGames.Sourcing.SourceEngine;

namespace SportGames.Sourcing;

public interface ISourceAnalyzer
{
    IEnumerable<SourcedGameInfo> Analyze();
}