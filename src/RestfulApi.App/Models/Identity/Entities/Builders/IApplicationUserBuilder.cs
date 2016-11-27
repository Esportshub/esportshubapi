using RestfulApi.App.Models.Esportshub.Builders;

namespace RestfulApi.App.Models.Identity.Entities.Builders
{
    public interface IApplicationUserBuilder : IBuilder<ApplicationUser>
    {
        IApplicationUserBuilder SetEmail(string input);
        IApplicationUserBuilder SetSalt(string input);
        IApplicationUserBuilder Verified(bool input);
        IApplicationUserBuilder SetChecksum(string input);
        IApplicationUserBuilder SetPassword(string input);
    }

}