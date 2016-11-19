using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EsportshubApi.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public static ApplicationUserBuilder Builder()
        {
            return new ApplicationUserBuilder(new ApplicationUser());
        }

        public string AccountId { get; set; }
        public Guid AccountGuid { get; set; }
        public string Salt { get; set; }
        public bool Verified { get; set; }
        public string Checksum { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

}
