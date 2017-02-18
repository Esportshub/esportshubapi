using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Constant;
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
        private const string GetGroup = "GetGroup";
        private const string GetGroups = "GetGroups";
        private const string UpdateGroup = "UpdateGroup";
        private const string CreateGroup = "CreateGroup";
        private const string DeleteGroup = "DeleteGroup";

        public GroupController(IGroupRepository groupRepository, ILogger<GroupController> logger, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = GetGroups)]
        public async Task<IActionResult> Get()
        {
            var groups = await _groupRepository.FindByAsync(group => group.GroupGuid == Guid.Empty, "");
            if (groups == null) return new NotFoundResult();
            var groupsDto = groups.Select(_mapper.Map<GroupDto>);
            return Json(groupsDto);
        }


        [HttpGet("{id}", Name = GetGroup)]
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

        [HttpPost(Name = CreateGroup)]
        public async Task<IActionResult> Create([FromBody] GroupDto groupDto)
        {
            if (groupDto == null) return BadRequest();
            var group = _mapper.Map<Group>(groupDto);
            _groupRepository.Insert(group);

            if (!await _groupRepository.SaveAsync())
                return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
            var groupResultDto = _mapper.Map<GroupDto>(group);
            return CreatedAtRoute(GetGroup, new {Id = groupResultDto.GroupGuid}, groupResultDto);
        }

        [HttpPatch("{id}", Name = UpdateGroup)]
        public async Task<IActionResult> Update(Guid id, [FromBody] GroupDto groupDto)
        {
            if (groupDto == null || Guid.Empty == id || groupDto.GroupGuid != id) return BadRequest();

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
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }

        [HttpDelete("{id}", Name = DeleteGroup)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _groupRepository.FindAsync(id);

            if (team == null) return NotFound();
            _groupRepository.Delete(id);
            if (await _groupRepository.SaveAsync())
            {
                return new NoContentResult();
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }
    }
}
