using System;
using EsportshubApi.Models.Entities;
using RestfulApi.Models.Validator;

namespace RestfulApi.Models.Identity.Entities.Account.Builder
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
        public ApplicationUser Build(AccountValidator validator)
        {
            validator.Validate(_applicationUser);
            return _applicationUser;
        }

        public IApplicationUserBuilder SetChecksum(string input)
        {
            _applicationUser.Checksum = input;
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


        public IApplicationUserBuilder Verified(bool input)
        {
            _applicationUser.Verified = input;
            return this;
        }

        public ApplicationUser Build(IValidator<ApplicationUser> validator)
        {
            throw new NotImplementedException();
        }

        //public Account Build(IValidator<Account> validator)
        //{
        //    throw new NotImplementedException();
        //}


    }
}