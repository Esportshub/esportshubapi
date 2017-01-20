using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            var teams = await _teamRepository.FindByAsync(null, "");
            return Ok(teams);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id) => Json(await _teamRepository.FindAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Team team)
        {
            if (team == null) return BadRequest();

            _teamRepository.Insert(team);
            return await _teamRepository.SaveAsync()
                ? CreatedAtRoute("GetPlayer", new {Id = team.TeamId}, team)
                : StatusCode(500, "Error while processing");
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromBody] Team team, int id)
        {
            if (team == null) return BadRequest();

            var _team = await _teamRepository.FindAsync(id);
            if (_team == null) return NotFound();

            _teamRepository.Update(team);
            return await _teamRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
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