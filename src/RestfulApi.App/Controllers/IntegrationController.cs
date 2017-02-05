using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Integrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Constant;
using RestfulApi.App.Dtos.ErrorDtos;
using RestfulApi.App.Dtos.IntegrationsDtos;

namespace RestfulApi.App.Controllers
{
    [Route("api/integrations")]
    public class IntegrationController : Controller
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly ILogger<IntegrationController> _logger;
        private readonly IMapper _mapper;
        private const string GetIntegration = "GetIntegration";
        private const string GetIntegrations = "GetIntegrations";
        private const string UpdateIntegration = "UpdateIntegration";
        private const string CreateIntegration = "CreateIntegration";
        private const string DeleteIntegration = "DeleteIntegration";

        public IntegrationController(IIntegrationRepository integrationRepository, ILogger<IntegrationController> logger, IMapper mapper)
        {
            _integrationRepository = integrationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = GetIntegrations)]
        public async Task<IActionResult> Get()
        {
            var integrations = await _integrationRepository.FindByAsync(integration => integration.Guid == Guid.Empty, "");
            if(integrations == null) return new NotFoundResult();
            var integrationsDto = integrations.Select(_mapper.Map<IntegrationDto>);
            return Json(integrationsDto);
        }

        [HttpGet("{id}", Name = GetIntegration)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (Guid.Empty == id)
            {
                return BadRequest(new InvalidRangeOnInputDto());
            }
            var integration = await _integrationRepository.FindAsync(id);
            if (integration == null) return NotFound();
            var integrationDto = _mapper.Map<IntegrationDto>(integration);
            return Json(integrationDto);
        }

        [HttpPost(Name = CreateIntegration)]
        public async Task<IActionResult> Create([FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();

            var integration = _mapper.Map<Integration>(integrationDto);

            _integrationRepository.Insert(integration);

            if (await _integrationRepository.SaveAsync())
            {
                var integrationResultDto = _mapper.Map<IntegrationDto>(integration);
                return new CreatedAtRouteResult(GetIntegration,
                    new {Id = integrationResultDto.IntegrationGuid}, integrationResultDto);
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }

        [HttpPatch("{id}", Name = UpdateIntegration)]
        public async Task<IActionResult> Update(Guid id, [FromBody] IntegrationDto integrationDto)
        {
            if (integrationDto == null) return BadRequest();

            var _ = await _integrationRepository.FindAsync(id);
            if (_ == null) return NotFound();
            Integration integration = _mapper.Map<Integration>(integrationDto);

            _integrationRepository.Update(integration);
            if (await _integrationRepository.SaveAsync())
            {
                var result = Ok(_mapper.Map<IntegrationDto>(integration));
                result.StatusCode = 200;
                return result;
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, "Internal server error");
        }

        [HttpDelete("{id}", Name = DeleteIntegration)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var integration = await _integrationRepository.FindAsync(id);

            if (integration == null) return NotFound();
            _integrationRepository.Delete(id);

            if (await _integrationRepository.SaveAsync())
            {
                return new NoContentResult();
            }
            return StatusCode((int) HttpStatusCode.InternalServerError, ErrorConstants.InternalServerError);
        }
    }
}
