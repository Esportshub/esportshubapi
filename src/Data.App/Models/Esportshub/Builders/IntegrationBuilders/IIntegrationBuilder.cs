using System;
using Data.App.Models.Esportshub.Entities;

namespace Data.App.Models.Esportshub.Builders.IntegrationBuilders
{
    public interface IIntegrationBuilder : IBuilder<Integration>
    {
        IIntegrationBuilder SetIntegrationGuid(Guid input);
        IIntegrationBuilder SetSocialMedia(SocialMedia input);
        IIntegrationBuilder SetPlayer(Player input);
    }

}