using EsportshubApi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace EsportshubApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
//            modelBuilder.Entity<ApplicationUserLogin>(entity =>
//            {
//                entity.ToTable("AspNetUserLogin", "Security");
//                entity.Property(e => e.UserId).HasColumnName("AspNetUserId");
//
//            });
//            modelBuilder.Entity<ApplicationRole>().HasKey(r => r.Id);
//            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new {r.RoleId, r.UserId});
        }
    }
}