using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Extensions;
using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Identity.Entities;
using RestfulApi.App.Models.Repositories.Players;
using RestfulApi.App.Services;

namespace RestfulApi.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Sets the url to lowercase
            //Do we allow Trailling slash?
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

            services.AddDbContext<EsportshubContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddMvc();
            services.SetIdentity();

            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.SetIdentityConfiguration();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseIdentity();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (
                    var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<EsportshubContext>().Database.Migrate();
                    EsportshubContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
                    serviceScope.ServiceProvider.GetService<EsportshubContext>().EnsureSeedData();
                }
            }

            app.UseMvc();
        }
    }
}