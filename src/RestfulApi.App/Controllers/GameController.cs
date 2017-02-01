using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ErrorDtos;
using RestfulApi.App.Dtos.EsportshubEventsDtos;
using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository, ILogger logger, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _gameRepository.FindByAsync(game => game.GameGuid == Guid.Empty, ""));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id)
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var game = await _gameRepository.FindAsync(id);
            if (game == null) return NotFound();
            var gameDto = _mapper.Map<EsportshubEventDto>(game);
            return Json(gameDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GameDto gameDto)
        {
            if (gameDto == null) return BadRequest();
            Game game = _mapper.Map<Game>(gameDto);

            _gameRepository.Insert(game);
            return await _gameRepository.SaveAsync()
                ? CreatedAtRoute("GetGame", new {Id = game.GameId}, game)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GameDto gameDto)
        {
            if (gameDto == null || gameDto.GameGuid != id) return BadRequest();

            var _ = await _gameRepository.FindAsync(id);
            if (_ == null) return NotFound();
            Game game = _mapper.Map<Game>(gameDto);

            _gameRepository.Update(game);
            return await _gameRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(Guid id)
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
