using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Constant;
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
        private const string GetGame = "GetGame";
        private const string GetGames = "GetGames";
        private const string UpdateGame = "UpdateGame";
        private const string CreateGame = "CreateGame";
        private const string DeleteGame = "DeleteGame";

        public GameController(IGameRepository gameRepository, ILogger logger, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = GetGames)]
        public async Task<IActionResult> Get()
        {
            var games = await _gameRepository.FindByAsync(game => game.GameGuid == Guid.Empty , "");
            if (games == null) return NotFound();
            IEnumerable<GameDto> gameDtos = games.Select(_mapper.Map<GameDto>);
            return Json(gameDtos);
        }

        [HttpGet("{id}", Name = GetGame)]
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

        [HttpPost(Name = CreateGame)]
        public async Task<IActionResult> Create([FromBody] GameDto gameDto)
        {
            if (gameDto == null) return BadRequest();
            Game game = _mapper.Map<Game>(gameDto);

            _gameRepository.Insert(game);
            if (await _gameRepository.SaveAsync())
            {
                var gameDtoResult = _mapper.Map<GameDto>(game);
                return CreatedAtRoute(GetGame, new {Id = game.GameGuid}, gameDtoResult);
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }

        [HttpPut("{id}", Name = UpdateGame)]
        public async Task<IActionResult> Update(Guid id, [FromBody] GameDto gameDto)
        {
            if (gameDto == null || gameDto.GameGuid != id) return BadRequest();

            var _ = await _gameRepository.FindAsync(id);
            if (_ == null) return NotFound();
            Game game = _mapper.Map<Game>(gameDto);

            _gameRepository.Update(game);
            if (await _gameRepository.SaveAsync())
            {
                var result = Ok(_mapper.Map<EsportshubEventDto>(game));
                result.StatusCode = 200;
                return result;
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }

        [HttpDelete("{id}", Name = DeleteGame)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var game = await _gameRepository.FindAsync(id);

            if (game == null) return NotFound();
            _gameRepository.Delete(id);
            if (await _gameRepository.SaveAsync())
            {
                return new NoContentResult();
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }
    }
}
