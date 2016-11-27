using System;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface IIntegrationBuilder : IBuilder<Integration>
    {
        IIntegrationBuilder SetIntegrationId(int input);
        IIntegrationBuilder SetIntegrationGuid(Guid input);
        IIntegrationBuilder SetName(Name input);
        IIntegrationBuilder SetCreated(DateTime input);
        IIntegrationBuilder SetUpdated(DateTime input);
        IIntegrationBuilder SetPlayer(Player input);

    }

}