using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.App.Dtos.PlayerDtos;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Constant;

namespace RestfulApi.App.Controllers
{
    [Route("api/players")]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<PlayerController> _logger;
        private readonly IMapper _mapper;
        private const string GetPlayer = "GetPlayer";
        private const string GetPlayers = "GetPlayers";
        private const string UpdatePlayer = "UpdatePlayer";
        private const string CreatePlayer = "CreatePlayer";
        private const string DeletePlayer = "DeletePlayer";

        public PlayerController(IPlayerRepository playerRepository, ILogger<PlayerController> logger, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = GetPlayers)]
        public async Task<IActionResult> Get()
        {
            var players = await _playerRepository.FindByAsync(player => player.PlayerId == 1 , "");
            if (players == null) return NotFound();
            var playerDtos = players.Select(_mapper.Map<PlayerDto>);
            return Json(playerDtos);
        }

        [HttpGet("{id}", Name = GetPlayer)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id) return BadRequest();
            var player = await _playerRepository.FindAsync(id);
            if (player == null) return NotFound();
            var result = _mapper.Map<PlayerDto>(player);
            return Json(result);
        }

        [HttpPost(Name = CreatePlayer)]
        public async Task<IActionResult> Create([FromBody] PlayerDto playerDto)
        {
            if (playerDto == null) return BadRequest();
            var player = _mapper.Map<Player>(playerDto);
            _playerRepository.Insert(player);
            if (!await _playerRepository.SaveAsync()) return StatusCode((int)HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
            var playerResultDto = _mapper.Map<PlayerDto>(player);
            return CreatedAtRoute(GetPlayer, new {Id = player.PlayerGuid}, playerResultDto);
        }

        [HttpPatch("{id}", Name = UpdatePlayer)]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlayerDto playerDto)
        {
            if (playerDto == null || Guid.Empty == id || playerDto.PlayerGuid != id) return BadRequest();

            var _ = await _playerRepository.FindAsync(id);
            if (_ == null) return NotFound();
            Player player = _mapper.Map<Player>(playerDto);
            _playerRepository.Update(player);
            if (await _playerRepository.SaveAsync())
            {
                return Ok(_mapper.Map<PlayerDto>(player));
            }
            return StatusCode(500, ErrorConstants.InternalServerError);
        }

        [HttpDelete("{id}", Name = DeletePlayer)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (Guid.Empty == id) return BadRequest();
            var player = await _playerRepository.FindAsync(id);
            if (player == null) return NotFound();
            _playerRepository.Delete(id);
            if (await _playerRepository.SaveAsync())
            {
                return new NoContentResult();
            }
            return StatusCode(500, ErrorConstants.InternalServerError);
        }
    }
}