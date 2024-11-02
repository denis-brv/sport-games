using SportGames.GamesProcessing;

var gamesProcessor = new object() as IGamesProcessor;
var queueHandler = new GamesQueueHandler(gamesProcessor);

/*
 * This is a background task/job application which constantly listens to the queue of parsed Game objects.
 * This is just an abstract definition, without real code.
 */
 
queueHandler.Read();