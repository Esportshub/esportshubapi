using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ErrorDtos;
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
        public async Task<IActionResult> Get()
        {
            var groups = await _groupRepository.FindByAsync(group => group.GroupGuid == Guid.Empty, "");
            if (groups == null) return new NotFoundResult();
            var groupsDto = groups.Select(_mapper.Map<GroupDto>);

            return Json(groupsDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id)
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var group = await _groupRepository.FindAsync(id);
            if (group == null) return NotFound();
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GroupDto groupDto)
        {
            if (groupDto == null) return BadRequest();

            var _ = await _groupRepository.FindAsync(id);
            if (_ == null) return NotFound();
            Group group = _mapper.Map<Group>(groupDto);

            _groupRepository.Update(group);
            if (await _groupRepository.SaveAsync())
            {
                var result = Ok(_mapper.Map<GroupDto>(group));
                result.StatusCode = 200;
                return result;
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
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
