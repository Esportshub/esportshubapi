using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Activities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ActivitiesDtos;

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
        public async Task<JsonResult> Get()

        {
            var activities = await _activityRepository.FindByAsync(null, "");
            var activitiesDto = activities.Select(_mapper.Map<ActivityDto>);

            return Json(activitiesDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid input");
            var activity = await _activityRepository.FindAsync(id);
            var activityDto = _mapper.Map<ActivityDto>(activity);
            return Json(activityDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityDto activityDto)
        {
            if (activityDto == null) return BadRequest();

            var activity = _mapper.Map<Activity>(activityDto);

            _activityRepository.Insert(activity);

            return await _activityRepository.SaveAsync()
                ? CreatedAtRoute("GetActivity", new {Id = activity.ActivityId}, activityDto)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ActivityDto activityDto)
        {
            if (activityDto == null) return BadRequest();

            var activity = await _activityRepository.FindAsync(activityDto.ActivityId);
            if (activity == null) return NotFound();

            _activityRepository.Update(activity);
            return await _activityRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _activityRepository.FindAsync(id);

            if (game == null) return NotFound();
            _activityRepository.Delete(id);
            return await _activityRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}