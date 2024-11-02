using SportGames.Sourcing;
using SportGames.Sourcing.SourceEngine;

/*
 * This is a background task/job application which periodically runs.
 */


var gamesQueue = new object() as IGamesQueue;
List<ISourceAnalyzer> analyzers = new List<ISourceAnalyzer>();
new SourcesAnalyzer(gamesQueue, analyzers).Run();