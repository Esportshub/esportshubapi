using System;
using System.IO;
using EsportshubApi.Models;
using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.Entity.Extensions;
using RestfulApi.Services;

namespace RestfulApi
{
    public class Startup
    {
    public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            services.AddEntityFrameworkSqlServer().AddMySQL().AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySQL(config["ConnectionStrings:DefaultConnection"]));

          /*  services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>() //,int
                .AddDefaultTokenProviders();*/

            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(config["ConnectionStrings:DefaultConnection"]));
            services.AddDbContext<EsportshubContext>(options => options.UseMySQL(config["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";

                // User settings
                options.User.RequireUniqueEmail = true;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<EsportshubContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<EsportshubContext>().EnsureSeedData();
                }
            }

            app.UseIdentity();

            app.UseMvc(routes =>
                 routes.MapRoute("player", "{controller=Player}/{action=Get}/{id?}")
            );
        }
    }
}
