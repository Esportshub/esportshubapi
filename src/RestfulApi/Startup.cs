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

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext,int>()
                .AddDefaultTokenProviders();

            services.AddMvcCore();
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(config["ConnectionStrings:DefaultConnection"]));
            services.AddDbContext<EsportshubContext>(options => options.UseMySQL(config["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
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
