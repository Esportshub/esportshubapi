using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using EsportshubApi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySQL.Data.Entity.Extensions;


namespace EsportshubApi.Models
{
//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

