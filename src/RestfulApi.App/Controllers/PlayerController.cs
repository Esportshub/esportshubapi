using System.Collections.Generic;
using System.Linq;
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
    //[Authorize]
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
<<<<<<< HEAD
        public async Task<IActionResult> Get([FromQuery] string playerIds, [FromQuery] string followers)
=======
        public async Task<IActionResult> Get([FromQuery] PlayerDto[] playerIds)
>>>>>>> a59e0491ff62f6e6c7b4540270b520c41b4641ab
        {
            IEnumerable<Player> players = await _playerRepository.FindByAsync();
            if (players == null) return NotFound();
            IEnumerable<PlayerDto> playerDtos = players.Select(_mapper.Map<PlayerDto>);
            return Json(playerDtos);
        }

<<<<<<< HEAD
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            int playerId;
            if (!int.TryParse(id.ToString(), out playerId))
            {
                return BadRequest(new InvalidInputTypeErrorDto());
            }
            if (!(playerId > 0))
=======
        [HttpGet("{inputId}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!(id > 0))
>>>>>>> a59e0491ff62f6e6c7b4540270b520c41b4641ab
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var player = await _playerRepository.FindAsync(playerId);
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

        [HttpPatch("{inputId")]
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

        [HttpDelete("{inputId")]
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