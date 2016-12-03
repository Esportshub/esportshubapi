using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Repositories.Games;

namespace RestfulApi.App.Controllers
{
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _gameRepository.GetAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return Json(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            if (game == null) return BadRequest();

            _gameRepository.Insert(game);
            return await _gameRepository.SaveAsync()
                ? CreatedAtRoute("GetPlayer", new {Id = game.GameId}, game)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] Game game)
        {
            if (game == null || game.GameId != id) return BadRequest();

            var _game = await _gameRepository.GetByIdAsync(id);
            if (_game == null) return NotFound();

            _gameRepository.Update(game);
            return await _gameRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game == null) return NotFound();
            _gameRepository.Delete(id);
            return await _gameRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}
