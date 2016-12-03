using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Repositories.Players;

namespace RestfulApi.App.Controllers
{
    [Authorize]
    [Route("api/players")]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(IPlayerRepository playerRepository, ILogger<PlayerController> logger)
        {
            _playerRepository = playerRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _playerRepository.FindByAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Get player was called!!!");
            return Json(await _playerRepository.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Player player)
        {
            if (player == null) return BadRequest();

            _playerRepository.Insert(player);
            return await _playerRepository.SaveAsync()
                ? CreatedAtRoute("GetPlayer", new {Id = player.PlayerId}, player)
                : StatusCode(500, "Error while processing");
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromBody] Player player, int id)
        {
            if (player == null) return BadRequest();

            var _player = await _playerRepository.FindAsync(id);
            if (_player == null) return NotFound();

            _playerRepository.Update(player);
            return await _playerRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerRepository.FindAsync(id);

            if (player == null) return NotFound();
            _playerRepository.Delete(id);
            return await _playerRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}