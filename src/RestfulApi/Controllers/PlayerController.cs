using System;
using System.Collections.Generic;
using EsportshubApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using EsportshubApi.Models.Entities;
using System.Threading.Tasks;

namespace EsportshubApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        internal IPlayerRepository _playerRepository;
        public PlayerController(IPlayerRepository playerRepository) 
        {
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Player> players = await _playerRepository.GetAsync(null, "");
            return Json(players);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            Console.WriteLine("This endpoint was hit");
            Player player = await _playerRepository.GetByIdAsync(id);
            return Json(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Player player)
        {
            if(player == null)
            {
                return BadRequest();
            }
            await _playerRepository.InsertAsync(player);
            return CreatedAtRoute("GetPlayer", new { Id = player.PlayerId}, player);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] Player player)
        {
            if(player == null || player.PlayerId !=id) 
            {
                return BadRequest();
            }
            Player _player = await _playerRepository.GetByIdAsync(id);
            if(_player == null) 
            {
                return NotFound();
            }
            await _playerRepository.UpdateAsync(player);
            return new NoContentResult();
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromBody] Player player, int id)
        {
            if(player == null)
            {
                return BadRequest();
            }
            Player _player = await _playerRepository.GetByIdAsync(id);
            if(_player == null)
            {
                return NotFound();
            }
            player.PlayerId = _player.PlayerId;
            await _playerRepository.UpdateAsync(player);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if(player == null)
            {
                return NotFound();
            }
            await _playerRepository.DeleteAsync(id);
            return new NoContentResult();
        }
    }
}
