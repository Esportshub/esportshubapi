using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ErrorDtos;
using RestfulApi.App.Dtos.PlayerDtos;
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
        public async Task<JsonResult> Get()
        {
            var teams = await _teamRepository.FindByAsync(null, "");
            var teamsDto = teams.Select(_mapper.Map<TeamDto>);
            return Json(teamsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!(id > 0))
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
            //Check if user exist
            _teamRepository.Insert(team);

            return await _teamRepository.SaveAsync()
                ? CreatedAtRoute("GetTeam??", new {Id = team.TeamId}, teamDto)
                : StatusCode(500, "Error while processing");
        }

<<<<<<< HEAD
        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id,[FromBody] TeamDto teamDto)
=======
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] TeamDto teamDto)
>>>>>>> origin/master
        {
            if (teamDto == null) return BadRequest();

            var team = await _teamRepository.FindAsync(teamDto.TeamId);
            if (team == null) return NotFound();

            _teamRepository.Update(team);
            return await _teamRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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