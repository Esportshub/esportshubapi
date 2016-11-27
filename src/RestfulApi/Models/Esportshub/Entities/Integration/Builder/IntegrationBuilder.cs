using System;
using RestfulApi.Models.Esportshub.Entities.Integration;
using RestfulApi.Models.Esportshub.Entities.Player;
using RestfulApi.Models.Validator;

namespace EsportshubApi.Models.Entities
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

        public Integration Build(IValidator<Integration> validator)
        {
            throw new NotImplementedException();
        }

        public Integration Build(IntegrationValidator validator)
        {
            validator.Validate(_integration);
            return _integration;
        }

        public IIntegrationBuilder SetIntegrationGuid(Guid input)
        {
            _integration.IntegrationGuid = input;
            return this;
        }

        public IIntegrationBuilder SetName(Name input)
        {
            _integration.Name = input;
            return this;
        }

        public IIntegrationBuilder SetPlayer(Player input)
        {
            _integration.Player = input;
            return this;
        }
    }
}