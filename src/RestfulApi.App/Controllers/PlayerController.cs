using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Repositories.Players;

namespace RestfulApi.App.Controllers
{
    [Route("api/player")]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _playerRepository.GetAsync(null, ""));
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            Console.WriteLine("player controller, with id:", id);
            return Json(await _playerRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Player player)
        {
            if (player == null) return BadRequest();

            _playerRepository.Insert(player);
            return  _playerRepository.SaveAsync().Result
                ? CreatedAtRoute("GetPlayer", new {Id = player.PlayerId}, player)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] Player player)
        {
            if (player == null || player.PlayerId != id) return BadRequest();

            var _player = await _playerRepository.GetByIdAsync(id);
            if (_player == null) return NotFound();

            _playerRepository.Update(player);
            return _playerRepository.SaveAsync().Result
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromBody] Player player, int id)
        {
            if (player == null) return BadRequest();

            var _player = await _playerRepository.GetByIdAsync(id);
            if (_player == null) return NotFound();

            _playerRepository.Update(player);
            return _playerRepository.SaveAsync().Result
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        // DELETE api/values/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);

            if (player == null) return NotFound();

             _playerRepository.Delete(id);
            return _playerRepository.SaveAsync().Result
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}