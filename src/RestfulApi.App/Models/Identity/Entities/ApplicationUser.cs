using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Identity.Entities.Builders;

namespace RestfulApi.App.Models.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public static ApplicationUserBuilder Builder()
        {
            return new ApplicationUserBuilder(new ApplicationUser());
        }

        public string ApplicationUserId { get; private set; }

        public Guid ApplicationUserGuid { get; private set; }

        public string Salt { get; set; }

        public Player Player { get; set; }

        public bool Verified { get; set; }

        public string Checksum { get; set; }

        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }
    }

}