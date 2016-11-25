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
        public IConfigurationRoot Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            services.AddOptions();

            services.AddEntityFrameworkSqlServer().AddMySQL().AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddMvc();
            //services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddDbContext<EsportshubContext>(options => options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<ApplicationUser, IdentityRole>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

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

            app.UseIdentity();

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
            ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();


            app.UseMvc(routes =>
                 routes.MapRoute("player", "{controller=Player}/{action=Get}/{id?}")
            );
        }
    }
}
