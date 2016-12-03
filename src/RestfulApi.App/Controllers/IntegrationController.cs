using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Repositories.Integrations;

namespace RestfulApi.App.Controllers
{
    [Route("api/integrations")]
    public class IntegrationController : Controller
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly ILogger<IntegrationController> _logger;

        public IntegrationController(IIntegrationRepository integrationRepository, ILogger<IntegrationController> logger)
        {
            _integrationRepository = integrationRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await _integrationRepository.FindByAsync(null, ""));

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id) => Json(await _integrationRepository.FindAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Integration integration)
        {
            if (integration == null) return BadRequest();

            _integrationRepository.Insert(integration);
            return await _integrationRepository.SaveAsync()
                ? CreatedAtRoute("GetRepository", new {Id = integration.IntegrationId}, integration)
                : StatusCode(500, "Error while processing");
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update([FromBody] Integration integration, int id)
        {
            if (integration == null) return BadRequest();

            var _integration = await _integrationRepository.FindAsync(id);
            if (_integration == null) return NotFound();

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
