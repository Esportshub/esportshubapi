using System;
using System.Collections.Generic;
using Models.Entites;
using Patterns.Builder;

namespace Models.Entities
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