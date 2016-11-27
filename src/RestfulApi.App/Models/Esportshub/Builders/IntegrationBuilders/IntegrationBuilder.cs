using System;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Esportshub.Builders.IntegrationBuilders
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


        public IIntegrationBuilder SetIntegrationGuid(Guid input)
        {
            _integration.IntegrationGuid = input;
            return this;
        }

        public IIntegrationBuilder SetSocialMedia(SocialMedia input)
        {
            _integration.SocialMedia = input;
            return this;
        }

        public IIntegrationBuilder SetPlayer(Player input)
        {
            _integration.Player = input;
            return this;
        }
    }
}