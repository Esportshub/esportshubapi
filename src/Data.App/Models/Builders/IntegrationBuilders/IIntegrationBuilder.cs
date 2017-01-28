using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.IntegrationBuilders
{
    public interface IIntegrationBuilder : IBuilder<Integration>
    {
        IIntegrationBuilder SetIntegrationGuid(Guid integrationGuid);
        IIntegrationBuilder SetSocialMedia(SocialMedia socialMedia);
        IIntegrationBuilder SetPlayer(Player player);
    }

}