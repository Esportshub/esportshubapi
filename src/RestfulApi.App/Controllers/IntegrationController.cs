using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Integrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.IntegrationsDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/integrations")]
    public class IntegrationController : Controller
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly ILogger<IntegrationController> _logger;
        private readonly IMapper _mapper;

        public IntegrationController(IIntegrationRepository integrationRepository, ILogger<IntegrationController> logger, IMapper mapper)
        {
            _integrationRepository = integrationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var integrations = await _integrationRepository.FindByAsync(null, "");
            var integrationsDto = integrations.Select(_mapper.Map<IntegrationDto>);

            return Json(integrationsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid input");

            var integration = await _integrationRepository.FindAsync(id);

            var integrationDto = _mapper.Map<IntegrationDto>(integration);

            return Json(integrationDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();

            var integration = _mapper.Map<Integration>(integrationDto);

            _integrationRepository.Insert(integration);

            return await _integrationRepository.SaveAsync()
                ? CreatedAtRoute("GetRepository", new {Id = integration.IntegrationId}, integrationDto)
                : StatusCode(500, "Error while processing");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();

            var integration = await _integrationRepository.FindAsync(integrationDto.IntegrationId);
            if (integration == null) return NotFound();

            _integrationRepository.Update(integration);
            return await _integrationRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var integration = await _integrationRepository.FindAsync(id);

            if (integration == null) return NotFound();
            _integrationRepository.Delete(id);
            return await _integrationRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }
    }
}
