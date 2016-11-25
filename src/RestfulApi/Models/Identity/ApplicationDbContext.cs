using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using EsportshubApi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySQL.Data.Entity.Extensions;


namespace EsportshubApi.Models
{
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

            /*modelBuilder.Entity<IdentityUserLogin<string>>()
                .Property(login => login.UserId)
                .ForMySQLHasColumnType("PK")
                .UseSqlServerIdentityColumn()
                .UseMySQLAutoIncrementColumn("AI");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(iul => iul.UserId);
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .Property(iul => iul.UserId).HasValueGenerator()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("CustomIdName");*/

            /*        modelBuilder.Entity<IdentityUserLogin<int>>()
                        .HasDiscriminator<string>("user_login_type")
                        .HasValue<IdentityUserLogin<int>>("user_login_base")
                        .HasValue<ApplicationUserLogin>("application_user_login");*/
        }
    }
}

