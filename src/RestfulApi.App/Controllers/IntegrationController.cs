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
        public async Task<IActionResult> Get() => Json(await _integrationRepository.FindByAsync(null, ""));
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id) => Json(await _integrationRepository.FindAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();
            Integration integration = _mapper.Map<Integration>(integrationDto);

            _integrationRepository.Insert(integration);
            return await _integrationRepository.SaveAsync()
                ? CreatedAtRoute("GetRepository", new {Id = integration.IntegrationId}, integration)
                : StatusCode(500, "Error while processing");
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();

            var _ = await _integrationRepository.FindAsync(id);
            if (_ == null) return NotFound();
            Integration integration = _mapper.Map<Integration>(integrationDto);

            _integrationRepository.Update(integration);
            return await _integrationRepository.SaveAsync()
                ? (IActionResult) new NoContentResult()
                : StatusCode(500, "Error while processing");
        }

        [HttpDelete("{id:int:min(1)}")]
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
