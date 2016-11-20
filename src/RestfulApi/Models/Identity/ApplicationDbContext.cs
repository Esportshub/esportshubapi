using System.Security.Claims;
using EsportshubApi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySQL.Data.Entity.Extensions;


namespace EsportshubApi.Models
{
    public class ApplicationDbContext<TUser> : IdentityDbContext<TUser> where TUser : IdentityUser
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext<ApplicationUser>> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(iul => iul.UserId);

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<ApplicationUser>().Property(u => u.PasswordHash).HasMaxLength(500);
            modelBuilder.Entity<ApplicationUser>().Property(u => u.PhoneNumber).HasMaxLength(50);

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");

            modelBuilder.Entity<IdentityUserClaim<string>>().HasDiscriminator<string>("type").HasValue<>;
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
//            modelBuilder.Entity<UserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
//            modelBuilder.Entity<UserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);
        }
    }
}