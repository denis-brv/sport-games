using Microsoft.AspNetCore.Mvc;
using SportGames.Infrastructure;

namespace SportGames.Controllers;

[ApiController]
[Route("games")]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;
    private readonly GameService _gameService;

    public GamesController(ILogger<GamesController> logger, GameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveGame([FromBody] Game game)
    {
        await _gameService.SaveGame(game);
        return Ok();
    }

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> Search([FromBody]SearchGamesFilter requestFilter)
    {
        var searchResult = await _gameService.SearchGames(requestFilter);
        return new JsonResult(searchResult);
    }
}
