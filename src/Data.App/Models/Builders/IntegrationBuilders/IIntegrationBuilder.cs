using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.IntegrationBuilders
{
    public interface IIntegrationBuilder : IBuilder<Integration>
    {
        IIntegrationBuilder SetIntegrationGuid(Guid input);
        IIntegrationBuilder SetSocialMedia(SocialMedia input);
        IIntegrationBuilder SetPlayer(Player input);
    }

}