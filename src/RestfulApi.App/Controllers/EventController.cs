using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.EsportshubEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.EsportshubEventsDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/events")]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEsportshubEventRepository _esportshubEventRepository;
        private readonly IMapper _mapper;

        public EventController(IEsportshubEventRepository esportshubEventRepository, ILogger<EventController> logger, IMapper mapper)
        {
            _logger = logger;
            _esportshubEventRepository = esportshubEventRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var esportshubEvents = await _esportshubEventRepository.FindByAsync(esportshubEvent => esportshubEvent.EsportshubEventGuid == Guid.Empty, "");
            if (esportshubEvents == null) return new NotFoundResult();
            var esportshubEventDtos = esportshubEvents.Select(_mapper.Map<EsportshubEventDto>);
            return Json(esportshubEventDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id) return BadRequest("Invalid input");

            var myEvent = await _esportshubEventRepository.FindAsync(id);
            var esportshubEventDto = _mapper.Map<EsportshubEventDto>(myEvent);
            return Json(esportshubEventDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EsportshubEventDto eventDto)
        {
            if (eventDto == null) return BadRequest();

            var @event = _mapper.Map<EsportshubEvent>(eventDto);

            _esportshubEventRepository.Insert(@event);
            return await _esportshubEventRepository.SaveAsync()
                ? CreatedAtRoute("GetEvent", new {Id = @event.EsportshubEventId}, eventDto)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] EsportshubEventDto esportshubEventDto)
        {
            if (esportshubEventDto == null) return BadRequest();

            var @event = await _esportshubEventRepository.FindAsync(esportshubEventDto.EsportshubEventGuid);
            if (@event == null) return NotFound();

            _esportshubEventRepository.Update(@event);
            return await _esportshubEventRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var _event = await _esportshubEventRepository.FindAsync(id);

            if (_event == null) return NotFound();
            _esportshubEventRepository.Delete(id);
            return await _esportshubEventRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}