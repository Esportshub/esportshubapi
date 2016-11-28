using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Extensions;
using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Repositories.Players;
using RestfulApi.App.Services;

namespace RestfulApi.App
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetStartUpConfiguration(env).Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Sets the url to lowercase
            //Do we allow Trailling slash?
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            services.AddDbContext<EsportshubContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc();
            services.AddIdentity();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.SetIdentityConfiguration();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseIdentity();
            app.UseMvc();

            if (!env.IsDevelopment()) return;
            app.UseDeveloperExceptionPage();
            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().Migrate(app);
            EsportshubContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().SeedData(app);
        }
    }
}