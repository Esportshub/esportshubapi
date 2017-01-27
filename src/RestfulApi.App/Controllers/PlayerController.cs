using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.App.Dtos.PlayerDtos;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ErrorDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/players")]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<PlayerController> _logger;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerRepository playerRepository, ILogger<PlayerController> logger, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //IEnumerable<Player> players = await _playerRepository.FindByAsync(player => playerIds.Contains(player.PlayerId), "");
            IEnumerable<Player> players = await _playerRepository.FindByAsync(player => player.PlayerId == 1 , "");
            if (players == null) return NotFound();
            IEnumerable<PlayerDto> playerDtos = players.Select(_mapper.Map<PlayerDto>);
            return Json(playerDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!(id > 0))
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var player = await _playerRepository.FindAsync(id);
            if (player == null) return NotFound();
            var result = _mapper.Map<PlayerDto>(player);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] Player inputPlayer, int id)
        {
            if (inputPlayer == null) return BadRequest();
            if (!(id > 0)) return BadRequest(new InvalidRangeOnInputDto());
            var player = await _playerRepository.FindAsync(id);
            if (player == null) return NotFound();
            _playerRepository.Update(player);
            return await _playerRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!(id > 0)) return BadRequest(new InvalidInputTypeErrorDto());
            var player = await _playerRepository.FindAsync(id);
            if (player == null) return NotFound();
            _playerRepository.Delete(id);
            return await _playerRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}