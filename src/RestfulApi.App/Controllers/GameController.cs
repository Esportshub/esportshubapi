using System.Threading.Tasks;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestfulApi.App.Controllers
{
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger _logger;

        public GameController(IGameRepository gameRepository, ILogger logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _gameRepository.FindByAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var game = await _gameRepository.FindAsync(id);
            return Json(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            if (game == null) return BadRequest();

            _gameRepository.Insert(game);
            return await _gameRepository.SaveAsync()
                ? CreatedAtRoute("GetGame", new {Id = game.GameId}, game)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] Game game)
        {
            if (game == null || game.GameId != id) return BadRequest();

            var _game = await _gameRepository.FindAsync(id);
            if (_game == null) return NotFound();

            _gameRepository.Update(game);
            return await _gameRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameRepository.FindAsync(id);

            if (game == null) return NotFound();
            _gameRepository.Delete(id);
            return await _gameRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}
