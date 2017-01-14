using System.Threading.Tasks;
using Data.App.Models.Esportshub.Entities;
using Data.App.Models.Repositories.Activities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestfulApi.App.Controllers
{
    [Route("api/activities")]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(IActivityRepository activityRepository, ILogger<ActivityController> logger)
        {
            _activityRepository = activityRepository;
            _logger = logger;
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
        public async Task<IActionResult> Create([FromBody] Activity activity)
        {
            if (activity == null) return BadRequest();

            _activityRepository.Insert(activity);
            return await _activityRepository.SaveAsync()
                ? CreatedAtRoute("GetActivity", new {Id = activity.ActivityId}, activity)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] Activity activity)
        {
            if (activity == null || activity.ActivityId != id) return BadRequest();

            var _activity = await _activityRepository.FindAsync(id);
            if (_activity == null) return NotFound();

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
