using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestfulApi.App.Controllers
{
    [Route("api/groups")]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupController(IGroupRepository groupRepository, ILogger<GroupController> logger, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _groupRepository.FindByAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id) => Json(await _groupRepository.FindAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Group group)
        {
            if (group == null) return BadRequest();

            _groupRepository.Insert(group);
            return await _groupRepository.SaveAsync()
                ? CreatedAtRoute("GetPlayer", new {Id = group.GroupId}, group)
                : StatusCode(500, "Error while processing");
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromBody] Group group, int id)
        {
            if (group == null) return BadRequest();

            var _group = await _groupRepository.FindAsync(id);
            if (_group == null) return NotFound();

            _groupRepository.Update(group);
            return await _groupRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _groupRepository.FindAsync(id);

            if (team == null) return NotFound();
            _groupRepository.Delete(id);
            return await _groupRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}
