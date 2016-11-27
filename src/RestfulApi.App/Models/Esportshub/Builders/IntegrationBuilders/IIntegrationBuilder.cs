using System;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Esportshub.Builders.IntegrationBuilders
{
    public interface IIntegrationBuilder : IBuilder<Integration>
    {
        IIntegrationBuilder SetIntegrationGuid(Guid input);
        IIntegrationBuilder SetSocialMedia(SocialMedia input);
        IIntegrationBuilder SetPlayer(Player input);
    }

}