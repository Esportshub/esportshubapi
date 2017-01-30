using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.GroupDtos;

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
        public async Task<JsonResult> Get()
        {
            var groups = await _groupRepository.FindByAsync(null, "");
            var groupsDto = groups.Select(_mapper.Map<GroupDto>);

            return Json(groupsDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid input");

            var group = await _groupRepository.FindAsync(id);
            var groupDto = _mapper.Map<GroupDto>(group);

            return Json(groupDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupDto groupDto)
        {
            if (groupDto == null) return BadRequest();

            var group = _mapper.Map<Group>(groupDto);

            _groupRepository.Insert(group);

            return await _groupRepository.SaveAsync()
                ? CreatedAtRoute("GetGroup", new {Id = group.GroupId}, groupDto)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GroupDto groupDto)
        {
            if (groupDto == null) return BadRequest();

            var group = await _groupRepository.FindAsync(groupDto.GroupId);
            if (group == null) return NotFound();

            _groupRepository.Update(group);
            return await _groupRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
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
