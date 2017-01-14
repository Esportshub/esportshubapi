using System.Threading.Tasks;
using Data.App.Models.Esportshub.Entities;
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

        public TeamController(ITeamRepository teamRepository, ILogger<TeamController> logger)
        {
            _teamRepository = teamRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _teamRepository.FindByAsync(null, ""));

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