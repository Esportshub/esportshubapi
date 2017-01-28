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

        public ActivityController(IActivityRepository activityRepository, ILogger<ActivityController> logger, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _logger = logger;
            _mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _activityRepository.FindByAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var game = await _activityRepository.FindAsync(id);
            return Json(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityDto activityDto)
        {
            if (activityDto == null) return BadRequest();
            Activity activity = _mapper.Map<Activity>(activityDto);

            _activityRepository.Insert(activity);
            return await _activityRepository.SaveAsync()
                ? CreatedAtRoute("GetActivity", new {Id = activity.ActivityId}, activity)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActivityDto activityDto)
        {
            if (activityDto == null || activityDto.ActivityId != id) return BadRequest();

            var _ = await _activityRepository.FindAsync(id);
            if (_ == null) return NotFound();

            Activity activity = _mapper.Map<Activity>(activityDto);
            _activityRepository.Update(activity);
            return await _activityRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
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
