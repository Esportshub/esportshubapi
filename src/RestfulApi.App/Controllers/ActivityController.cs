using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Activities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Constant;
using RestfulApi.App.Dtos.ActivitiesDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/activities")]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;

//        private readonly ILogger<ActivityController> _logger;
        private readonly IMapper _mapper;

        private const string GetActivity = "GetActivity";
        private const string GetActivities = "GetActivities";
        private const string UpdateActivity = "UpdateActivity";
        private const string CreateActivity = "CreateActivity";
        private const string DeleteActivity = "DeleteActivity";

        public ActivityController(IActivityRepository activityRepository, ILogger<ActivityController> logger,
            IMapper mapper)
        {
            _activityRepository = activityRepository;
//            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = GetActivities)]
        public async Task<IActionResult> Get()
        {
            var activities = await _activityRepository.FindByAsync(activity => activity.ActivityGuid == Guid.Empty, "");
            if (activities == null) return new NotFoundResult();
            var activityDtos = activities.Select(_mapper.Map<ActivityDto>);
            return Json(activityDtos);
        }

        [HttpGet("{id}", Name = GetActivity)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id) return BadRequest();
            var activity = await _activityRepository.FindAsync(id);
            if (activity == null) return NotFound();
            var activityDto = _mapper.Map<ActivityDto>(activity);
            return Json(activityDto);
        }

        [HttpPost(Name = CreateActivity)]
        public async Task<IActionResult> Create([FromBody] ActivityDto activityDto)
        {
            if (activityDto == null)
            {
                return new BadRequestResult();
            }
            var activity = _mapper.Map<Activity>(activityDto);

            _activityRepository.Insert(activity);

            if (!await _activityRepository.SaveAsync())
                return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);

            var activityResultDto = _mapper.Map<ActivityDto>(activity);
            return CreatedAtRoute(GetActivity, new {Id = activity.ActivityGuid}, activityResultDto);
        }

        [HttpPut("{id}", Name = UpdateActivity)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ActivityDto activityDto)
        {
            if (activityDto == null || Guid.Empty == id || activityDto.ActivityGuid != id) return BadRequest();

            if (await _activityRepository.FindAsync(id) == null) return NotFound();

            var activity = _mapper.Map<Activity>(activityDto);
            _activityRepository.Update(activity);

            if (!await _activityRepository.SaveAsync())
                return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);

            var result = Ok(_mapper.Map<ActivityDto>(activity));
            result.StatusCode = 200;
            return result;
        }

        [HttpDelete("{id}", Name = DeleteActivity)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var activity = await _activityRepository.FindAsync(id);

            if (activity == null) return NotFound();
            _activityRepository.Delete(id);
            if (await _activityRepository.SaveAsync())
            {
                return new NoContentResult();
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }
    }
}