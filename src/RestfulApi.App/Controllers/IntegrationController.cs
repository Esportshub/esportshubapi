using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Integrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using RestfulApi.App.Dtos.ErrorDtos;
using RestfulApi.App.Dtos.GroupDtos;
=======
>>>>>>> origin/master
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
<<<<<<< HEAD
        public async Task<IActionResult> Get() => Json(await _integrationRepository.FindByAsync(null, ""));
=======
        public async Task<JsonResult> Get()
        {
            var integrations = await _integrationRepository.FindByAsync(null, "");
            var integrationsDto = integrations.Select(_mapper.Map<IntegrationDto>);

            return Json(integrationsDto);
        }
>>>>>>> origin/master

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
<<<<<<< HEAD
            if (!(id > 0))
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var integration = await _integrationRepository.FindAsync(id);
            if (integration == null) return NotFound();
            var integrationDto = _mapper.Map<IntegrationDto>(integration);
            return Json(integrationDto);
        }
=======
            if (id <= 0) return BadRequest("Invalid input");

            var integration = await _integrationRepository.FindAsync(id);

            var integrationDto = _mapper.Map<IntegrationDto>(integration);

            return Json(integrationDto);
        }

>>>>>>> origin/master

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();
<<<<<<< HEAD
            Integration integration = _mapper.Map<Integration>(integrationDto);
=======

            var integration = _mapper.Map<Integration>(integrationDto);
>>>>>>> origin/master

            _integrationRepository.Insert(integration);

            return await _integrationRepository.SaveAsync()
                ? CreatedAtRoute("GetRepository", new {Id = integration.IntegrationId}, integrationDto)
                : StatusCode(500, "Error while processing");
        }

<<<<<<< HEAD
        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();

            var _ = await _integrationRepository.FindAsync(id);
            if (_ == null) return NotFound();
            Integration integration = _mapper.Map<Integration>(integrationDto);
=======
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();

            var integration = await _integrationRepository.FindAsync(integrationDto.IntegrationId);
            if (integration == null) return NotFound();
>>>>>>> origin/master

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
