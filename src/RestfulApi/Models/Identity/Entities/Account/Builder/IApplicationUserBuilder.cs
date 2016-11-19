using System;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities 
{
    public interface IApplicationUserBuilder : IBuilder<ApplicationUser, AccountValidator>
    {
        IApplicationUserBuilder SetAccountGuid(Guid input);
        IApplicationUserBuilder SetEmail(string input);
        IApplicationUserBuilder SetSalt(string input);
        IApplicationUserBuilder Verified(bool input);
        IApplicationUserBuilder SetChecksum(string input);
        IApplicationUserBuilder SetPassword(string input);
        IApplicationUserBuilder SetCreated(DateTime input);
        IApplicationUserBuilder SetUpdated(DateTime input);
    }

}