using EsportshubApi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace EsportshubApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUserLogin>(entity =>
            {
                entity.Property(s => s.UserId);
                entity.HasKey(s => s.UserId);
            });
            modelBuilder.Entity<ApplicationUser>().HasKey(r => r.Id);
            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new {r.RoleId, r.UserId});
        }
    }
}