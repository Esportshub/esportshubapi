using EsportshubApi.Models.Entities;
using RestfulApi.Patterns.Builder;

namespace RestfulApi.Models.Identity.Entities.Account.Builder
{
    public interface IApplicationUserBuilder : IBuilder<ApplicationUser, AccountValidator>
    {
        IApplicationUserBuilder SetEmail(string input);
        IApplicationUserBuilder SetSalt(string input);
        IApplicationUserBuilder Verified(bool input);
        IApplicationUserBuilder SetChecksum(string input);
        IApplicationUserBuilder SetPassword(string input);
    }

}