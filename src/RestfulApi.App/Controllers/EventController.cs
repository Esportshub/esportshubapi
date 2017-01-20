using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities.Events;
using Data.App.Models.Repositories.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestfulApi.App.Controllers
{
    [Route("api/events")]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository eventRepository, ILogger<EventController> logger, IMapper mapper)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _eventRepository.FindByAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var game = await _eventRepository.FindAsync(id);
            return Json(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Event inputEvent)
        {
            if (inputEvent == null) return BadRequest();

            _eventRepository.Insert(inputEvent);
            return await _eventRepository.SaveAsync()
                ? CreatedAtRoute("GetEvent", new {Id = inputEvent.EventId}, inputEvent)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] Event inputEvent)
        {
            if (inputEvent == null || inputEvent.EventId != id) return BadRequest();

            var _event = await _eventRepository.FindAsync(id);
            if (_event == null) return NotFound();

            _eventRepository.Update(inputEvent);
            return await _eventRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _event = await _eventRepository.FindAsync(id);

            if (_event == null) return NotFound();
            _eventRepository.Delete(id);
            return await _eventRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}
