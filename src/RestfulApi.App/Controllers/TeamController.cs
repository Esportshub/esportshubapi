using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Constant;
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
        private const string GetTeam = "GetTeam";
        private const string GetTeams = "GetTeams";
        private const string UpdateTeam = "UpdateTeam";
        private const string CreateTeam = "CreateTeam";
        private const string DeleteTeam = "DeleteTeam";

        public TeamController(ITeamRepository teamRepository, ILogger<TeamController> logger, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = GetTeams)]
        public async Task<IActionResult> Get()
        {
            var teams = await _teamRepository.FindByAsync(team => team.TeamGuid == Guid.Empty, "");
            if (teams == null) return new NotFoundResult();
            var teamDtos = teams.Select(_mapper.Map<TeamDto>);
            return Json(teamDtos);
        }

        [HttpGet("{id}", Name = GetTeam)]
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

        [HttpPost(Name = CreateTeam)]
        public async Task<IActionResult> Create([FromBody] TeamDto teamDto)
        {
            if (teamDto == null) return BadRequest("Invalid input");

            var team = _mapper.Map<Team>(teamDto);

            _teamRepository.Insert(team);

            if (await _teamRepository.SaveAsync())
            {
                var teamResultDto = _mapper.Map<TeamDto>(team);
                return CreatedAtRoute(GetTeam, new {Id = teamResultDto.TeamGuid}, teamResultDto);
            }
            return StatusCode(500, ErrorConstants.InternalServerError);
        }


        [HttpPatch("{id}", Name = UpdateTeam)]
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
            return StatusCode(500, ErrorConstants.InternalServerError);
        }

        [HttpDelete("{id}", Name = DeleteTeam)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _teamRepository.FindAsync(id);

            if (team == null) return NotFound();
            _teamRepository.Delete(id);
            if (await _teamRepository.SaveAsync())
            {
                return new NoContentResult();
            }
            return StatusCode(500, ErrorConstants.InternalServerError);
        }
    }
}