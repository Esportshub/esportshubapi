using System;
using EsportshubApi.Models;
using EsportshubApi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EsportshubApi.Models.Entities
{
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {

    }
public class ApplicationUserRole : IdentityUserRole<int> { }
public class ApplicationUserClaim : IdentityUserClaim<int> { }

public sealed class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole() { }
    public ApplicationRole(string name) { Name = name; }
}

public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>
{
    public ApplicationUserStore(ApplicationDbContext context) : base(context) { }
}

public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, int>
{
    public ApplicationRoleStore(ApplicationDbContext context) : base(context) { }
}
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser() { }

        public static ApplicationUserBuilder Builder()
        {
            return new ApplicationUserBuilder(new ApplicationUser());

        }

        public int AccountId { get; set; }
        public Guid AccountGuid { get; set; }
        public string Salt { get; set; }
        public bool Verified { get; set; }
        public string Checksum { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

}
