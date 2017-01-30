using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.EsportshubEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ErrorDtos;
using RestfulApi.App.Dtos.EsportshubEventsDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/events")]
    public class EsportshubEventController : Controller
    {
        private readonly ILogger<EsportshubEventController> _logger;
        private readonly IEsportshubEventRepository _esportshubEventRepository;
        private readonly IMapper _mapper;

        public EsportshubEventController(IEsportshubEventRepository esportshubEventRepository, ILogger<EsportshubEventController> logger, IMapper mapper)
        {
            _logger = logger;
            _esportshubEventRepository = esportshubEventRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _esportshubEventRepository.FindByAsync(null, ""));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!(id > 0))
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var esportshubEvent = await _esportshubEventRepository.FindAsync(id);
            if (esportshubEvent == null) return NotFound();
            var esportshubEventDto = _mapper.Map<EsportshubEventDto>(esportshubEvent);
            return Json(esportshubEventDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EsportshubEventDto eventDto)
        {
            if (eventDto == null) return BadRequest();
            EsportshubEvent esportshubEvent = _mapper.Map<EsportshubEvent>(eventDto);

            _esportshubEventRepository.Insert(esportshubEvent);
            return await _esportshubEventRepository.SaveAsync()
                ? CreatedAtRoute("GetEvent", new {Id = esportshubEvent.EsportshubEventId})
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EsportshubEventDto esportshubEventDto)
        {
            if (esportshubEventDto == null || esportshubEventDto.EsportshubEventId != id) return BadRequest();

            var _ = await _esportshubEventRepository.FindAsync(id);
            if (_ == null) return NotFound();
            EsportshubEvent esportshubEvent = _mapper.Map<EsportshubEvent>(esportshubEventDto);

            _esportshubEventRepository.Update(esportshubEvent);
            return await _esportshubEventRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
