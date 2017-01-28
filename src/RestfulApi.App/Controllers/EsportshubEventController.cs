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
    public class EsportshubEventController : Controller
    {
        private readonly ILogger<EsportshubEventController> _logger;
        private readonly IEsportshubEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EsportshubEventController(IEsportshubEventRepository esportshubEventRepository, ILogger<EsportshubEventController> logger, IMapper mapper)
        {
            _logger = logger;
            _eventRepository = esportshubEventRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _eventRepository.FindByAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            EsportshubEvent esportshubEvent = await _eventRepository.FindAsync(id);
            var eventDto = _mapper.Map<EsportshubEventDto>(esportshubEvent);
            return Json(eventDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EsportshubEventDto eventDto)
        {
            if (eventDto == null) return BadRequest();
            EsportshubEvent esportshubEvent = _mapper.Map<EsportshubEvent>(eventDto);

            _eventRepository.Insert(esportshubEvent);
            return await _eventRepository.SaveAsync()
                ? CreatedAtRoute("GetEvent", new {Id = esportshubEvent.EventId})
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] EsportshubEventDto esportshubEventDto)
        {
            if (esportshubEventDto == null || esportshubEventDto.EsportshubEventId != id) return BadRequest();

            var _ = await _eventRepository.FindAsync(id);
            if (_ == null) return NotFound();
            EsportshubEvent esportshubEvent = _mapper.Map<EsportshubEvent>(esportshubEventDto);

            _eventRepository.Update(esportshubEvent);
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
