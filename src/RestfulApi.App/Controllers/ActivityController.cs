using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Activities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ActivitiesDtos;
using RestfulApi.App.Dtos.ErrorDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/activities")]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger<ActivityController> _logger;
        private readonly IMapper _mapper;

        public ActivityController(IActivityRepository activityRepository, ILogger<ActivityController> logger,
            IMapper mapper)
        {
            _activityRepository = activityRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var activities = await _activityRepository.FindByAsync(activity => activity.ActivityGuid == Guid.Empty, "");
            if (activities == null) return new NotFoundResult();
            var activityDtos = activities.Select(_mapper.Map<ActivityDto>);
            return Json(activityDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id)
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var activity = await _activityRepository.FindAsync(id);
            if (activity == null) return NotFound();
            var activityDto = _mapper.Map<ActivityDto>(activity);
            return Json(activityDto);
         }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityDto activityDto)
        {
            if (activityDto == null)
            {
                return new BadRequestResult();
            }
            var activity = _mapper.Map<Activity>(activityDto);

            _activityRepository.Insert(activity);

            if (await _activityRepository.SaveAsync())
            {
                return new CreatedAtRouteResult("Get", new {Id = activity.ActivityGuid}, activityDto);
            }
            return StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ActivityDto activityDto)
        {
            if (activityDto == null || activityDto.ActivityGuid != id) return BadRequest();

            var _ = await _activityRepository.FindAsync(id);
            if (_ == null) return NotFound();

            Activity activity = _mapper.Map<Activity>(activityDto);
            _activityRepository.Update(activity);
            if (await _activityRepository.SaveAsync())
            {
                var result = Ok(_mapper.Map<ActivityDto>(activity));
                result.StatusCode = 200;
                return result;
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var activity = await _activityRepository.FindAsync(id);

            if (activity == null) return NotFound();
            _activityRepository.Delete(id);
            return await _activityRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}