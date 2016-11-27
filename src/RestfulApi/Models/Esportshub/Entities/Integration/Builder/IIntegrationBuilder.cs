using System;
using RestfulApi.Models.Esportshub.Entities.Integration;
using RestfulApi.Models.Esportshub.Entities.Player;
using RestfulApi.Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface IIntegrationBuilder : IBuilder<Integration, IntegrationValidator>
    {
        IIntegrationBuilder SetIntegrationGuid(Guid input);
        IIntegrationBuilder SetName(Name input);
        IIntegrationBuilder SetPlayer(Player input);
    }

}