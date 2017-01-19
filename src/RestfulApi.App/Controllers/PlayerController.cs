using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.App.Dtos.PlayerDtos;
using Microsoft.Extensions.Logging;

namespace RestfulApi.App.Controllers
{
//    [Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            int validId;
            if (!int.TryParse(id, out validId))
            {
                return N

            }
            var playerEntity = await _playerRepository.FindAsync(validId);
            if (playerEntity == null) return NotFound("No objects");
            var result = Mapper.Map<PlayerDto>(playerEntity);
            return Json(result);
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

        [HttpPatch("{id")]
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

        [HttpDelete("{id")]
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