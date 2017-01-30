using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities.Events;
using Data.App.Models.Repositories.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.EventsDtos;

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
        public async Task<JsonResult> Get()
        {
            var myEvent = await _eventRepository.FindByAsync(null, "");
            var eventsDto = myEvent.Select(_mapper.Map<EventDto>);
            return Json(eventsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid input");

            var myEvent = await _eventRepository.FindAsync(id);
            var eventDto = _mapper.Map<EventDto>(myEvent);
            return Json(eventDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventDto eventDto)
        {
            if (eventDto == null) return BadRequest();

            var @event = _mapper.Map<Event>(eventDto);

            _eventRepository.Insert(@event);
            return await _eventRepository.SaveAsync()
                ? CreatedAtRoute("GetEvent", new {Id = @event.EventId}, eventDto)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] EventDto eventDto)
        {
            if (eventDto == null) return BadRequest();

            var @event = await _eventRepository.FindAsync(eventDto.EventId);
            if (@event == null) return NotFound();

            _eventRepository.Update(@event);
            return await _eventRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
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