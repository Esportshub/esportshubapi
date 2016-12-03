using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Repositories.Teams;

namespace RestfulApi.App.Controllers
{
    [Route("api/teams")]
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
//        public async Task<IActionResult> Get() => Json(await _teamRepository.GetAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id) => Json(await _teamRepository.GetByIdAsync(id));

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

            var _team = await _teamRepository.GetByIdAsync(id);
            if (_team == null) return NotFound();

            _teamRepository.Update(team);
            return await _teamRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null) return NotFound();
            _teamRepository.Delete(id);
            return await _teamRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}
