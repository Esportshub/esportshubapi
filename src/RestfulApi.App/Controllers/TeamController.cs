using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ErrorDtos;
using RestfulApi.App.Dtos.TeamDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/teams")]
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;

        public TeamController(ITeamRepository teamRepository, ILogger<TeamController> logger, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teams = await _teamRepository.FindByAsync(team => team.TeamGuid == Guid.Empty, "");
            if (teams == null) return new NotFoundResult();
            var teamDtos = teams.Select(_mapper.Map<TeamDto>);
            return Json(teamDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id)
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var team = await _teamRepository.FindAsync(id);
            if (team == null) return NotFound();
            var teamDto = _mapper.Map<TeamDto>(team);
            return Json(teamDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamDto teamDto)
        {
            if (teamDto == null) return BadRequest("Invalid input");

            var team = _mapper.Map<Team>(teamDto);

            _teamRepository.Insert(team);

            return await _teamRepository.SaveAsync()
                ? CreatedAtRoute("GetTeam", new {Id = team.TeamId}, teamDto)
                : StatusCode(500, "Error while processing");
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TeamDto teamDto)
        {
            if (teamDto == null) return BadRequest();

            var team = await _teamRepository.FindAsync(teamDto.TeamGuid);
            if (team == null) return NotFound();

            _teamRepository.Update(team);
            if (await _teamRepository.SaveAsync())
            {
                var result = Ok(_mapper.Map<TeamDto>(team));
                result.StatusCode = 200;
                return result;
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _teamRepository.FindAsync(id);

            if (team == null) return NotFound();
            _teamRepository.Delete(id);
            return await _teamRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}