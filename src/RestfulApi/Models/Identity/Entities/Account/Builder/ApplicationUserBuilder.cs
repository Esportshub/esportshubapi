using System;

namespace EsportshubApi.Models.Entities
{
    public class ApplicationUserBuilder : IApplicationUserBuilder
    {
        private ApplicationUser _applicationUser;
        
        public ApplicationUserBuilder(ApplicationUser applicationUser)
        {
            _applicationUser = applicationUser;
        }
        public ApplicationUser Build()
        {
           return _applicationUser;
        }

        /*public IAccountBuilder AccountId(string input)
        {
            _account.AccountId = input;
            return this;
        }*/


        public IApplicationUserBuilder SetAccountGuid(Guid input)
        {
            _applicationUser.AccountGuid = input;
            return this;
        }

        public IApplicationUserBuilder SetChecksum(string input)
        {
            _applicationUser.Checksum = input;
            return this;
        }

        public IApplicationUserBuilder SetCreated(DateTime input)
        {
            _applicationUser.Created = input;
            return this;
        }

        public IApplicationUserBuilder SetEmail(string input)
        {
            _applicationUser.Email = input;
            return this;
        }

        public IApplicationUserBuilder SetPassword(string input)
        {
            _applicationUser.Password  = input;
            return this;
        }

        public IApplicationUserBuilder SetSalt(string input)
        {
            _applicationUser.Salt = input;
            return this;
        }

        public IApplicationUserBuilder SetUpdated(DateTime input)
        {
            _applicationUser.Updated = input;
            return this;
        }

        public IApplicationUserBuilder Verified(bool input)
        {
            _applicationUser.Verified = input;
            return this;
        }

    }
}