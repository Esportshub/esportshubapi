using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EsportshubApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration) {
            UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            Console.WriteLine("Name:", configuration["Data:AdminUser:Name"]);
            Console.WriteLine("Email:", configuration["Data:AdminUser:Email"]);
            string username = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            string role = configuration["Data:AdminUser:Role"];

            if (await userManager.FindByNameAsync(username) == null) {
                if (await roleManager.FindByNameAsync(role) == null) {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                ApplicationUser user = new ApplicationUser {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
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

