using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.IntegrationBuilders
{
    public class IntegrationBuilder : IIntegrationBuilder
    {
        private Integration _integration;
        public IntegrationBuilder (Integration integration)
        {
           _integration = integration;
        }

        public Integration Build()
        {
            throw new NotImplementedException();
        }


        public IIntegrationBuilder SetIntegrationGuid(Guid integrationGuid)
        {
            _integration.IntegrationGuid = integrationGuid;
            return this;
        }

        public IIntegrationBuilder SetSocialMedia(SocialMedia socialMedia)
        {
            _integration.SocialMedia = socialMedia;
            return this;
        }

        public IIntegrationBuilder SetPlayer(Player input)
        {
            _integration.Player = input;
            return this;
        }
    }
}