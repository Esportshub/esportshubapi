using System;
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

        public IIntegrationBuilder SetCreated(DateTime input)
        {
           _integration.Created = input;
           return this;
        }

        public IIntegrationBuilder SetIntegrationGuid(Guid input)
        {
            _integration.IntegrationGuid = input;
            return this;
        }

        public IIntegrationBuilder SetIntegrationId(int input)
        {
            _integration.IntegrationId = input;
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

        public IIntegrationBuilder SetUpdated(DateTime input)
        {
            _integration.Updated = input;
            return this;
        }
    }
}