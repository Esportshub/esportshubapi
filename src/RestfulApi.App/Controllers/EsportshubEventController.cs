using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.EsportshubEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Constant;
using RestfulApi.App.Dtos.ErrorDtos;
using RestfulApi.App.Dtos.EsportshubEventsDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/esportshubevents")]
    public class EsportshubEventController : Controller
    {
        private readonly ILogger<EsportshubEventController> _logger;
        private readonly IEsportshubEventRepository _esportshubEventRepository;
        private readonly IMapper _mapper;
        private const string GetEsportshubEvent = "GetEsportshubEvent";
        private const string GetEsportshubEvents = "GetEsportshubEvents";
        private const string UpdateEsportshubEvent = "UpdateEsportshubEvent";
        private const string CreateEsportshubEvent = "CreateEsportshubEvent";
        private const string DeleteEsportshubEvent = "DeleteEsportshubEvent";

        public EsportshubEventController(IEsportshubEventRepository esportshubEventRepository, ILogger<EsportshubEventController> logger, IMapper mapper)
        {
            _logger = logger;
            _esportshubEventRepository = esportshubEventRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = GetEsportshubEvents)]
        public async Task<IActionResult> Get()
        {
            var esportshubEvents = await _esportshubEventRepository.FindByAsync(esportshubEvent => esportshubEvent.EsportshubEventGuid == Guid.Empty , "");
            if (esportshubEvents == null) return NotFound();
            var esportshubEventDtos = esportshubEvents.Select(esportshubEvent => _mapper.Map<EsportshubEventDto>(esportshubEvent)).ToList();
            return Json(esportshubEventDtos);
        }

        [HttpGet("{id}", Name = GetEsportshubEvent)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id) return BadRequest(new InvalidRangeOnInputDto());
            var esportshubEvent = await _esportshubEventRepository.FindAsync(id);
            if (esportshubEvent == null) return NotFound();
            var esportshubEventDto = _mapper.Map<EsportshubEventDto>(esportshubEvent);
            return Json(esportshubEventDto);
        }

        [HttpPost(Name = CreateEsportshubEvent)]
        public async Task<IActionResult> Create([FromBody] EsportshubEventDto esportshubEventDto)
        {
            if (esportshubEventDto == null) return BadRequest();
            EsportshubEvent esportshubEvent = _mapper.Map<EsportshubEvent>(esportshubEventDto);

            _esportshubEventRepository.Insert(esportshubEvent);
            if (await _esportshubEventRepository.SaveAsync())
            {
                var esportshubEventDtoResult = _mapper.Map<EsportshubEventDto>(esportshubEvent);
                return CreatedAtRoute(GetEsportshubEvent, new {Id = esportshubEvent.EsportshubEventGuid},
                    esportshubEventDtoResult);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }

        [HttpPatch("{id}", Name = UpdateEsportshubEvent)]
        public async Task<IActionResult> Update(Guid id, [FromBody] EsportshubEventDto esportshubEventDto)
        {
            if (esportshubEventDto == null || Guid.Empty == id || esportshubEventDto.EsportshubEventGuid != id) return BadRequest();

            var _ = await _esportshubEventRepository.FindAsync(id);
            if (_ == null) return NotFound();
            var esportshubEvent = _mapper.Map<EsportshubEvent>(esportshubEventDto);

            _esportshubEventRepository.Update(esportshubEvent);
            if (await _esportshubEventRepository.SaveAsync())
            {
                return Ok(_mapper.Map<EsportshubEventDto>(esportshubEvent));
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }

        [HttpDelete("{id}", Name = DeleteEsportshubEvent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var _event = await _esportshubEventRepository.FindAsync(id);

            if (_event == null) return NotFound();
            _esportshubEventRepository.Delete(id);
            if (await _esportshubEventRepository.SaveAsync())
            {
                return new NoContentResult();
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }
    }
}
