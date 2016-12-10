using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestfulApi.App.Dtos.ActivitiesDtos;
using RestfulApi.App.Dtos.EventsDtos;
using RestfulApi.App.Dtos.GameDtos;
using RestfulApi.App.Dtos.GroupDtos;
using RestfulApi.App.Dtos.IntegrationsDtos;
using RestfulApi.App.Dtos.PlayerDtos;
using RestfulApi.App.Dtos.SocialMediaDtos;
using RestfulApi.App.Dtos.TeamDtos;
using RestfulApi.App.Extensions;
using RestfulApi.App.Logging;
using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;
using RestfulApi.App.Models.Repositories.Activities;
using RestfulApi.App.Models.Repositories.Events;
using RestfulApi.App.Models.Repositories.Games;
using RestfulApi.App.Models.Repositories.Groups;
using RestfulApi.App.Models.Repositories.Integrations;
using RestfulApi.App.Models.Repositories.Players;
using RestfulApi.App.Models.Repositories.Teams;
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
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            services.AddDbContext<EsportshubContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc();
            services.AddDbContext<EsportshubContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc(options =>
            {
                options.Filters.Add(new LogAsyncActionFilter());
            });
            services.AddIdentity();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IIntegrationRepository, IntegrationRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.SetIdentityConfiguration();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //Activity => Dto
                cfg.CreateMap<Activity, ActivityDto>();
                //Event => Dto
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<GameEvent, GameEventDto>();
                cfg.CreateMap<GroupEvent, GroupEventDto>();
                cfg.CreateMap<TeamEvent, TeamEventDto>();
                //Game => Dto
                cfg.CreateMap<Game, GameDto>();
                //Group =>  Dto
                cfg.CreateMap<Group, GroupDto>();
                //Integration => Dto
                cfg.CreateMap<Integration, IntegrationDto>();
                //Player => Dto
                cfg.CreateMap<Player, PlayerDto>();
                cfg.CreateMap<PlayerGames, PlayerGamesDto>();
                cfg.CreateMap<PlayerGroups, PlayerGroupsDto>();
                cfg.CreateMap<PlayerTeams, PlayerTeamsDto>();
                //SocialMedia => Dto
                cfg.CreateMap<SocialMedia, SocialMediaDto>();
                //Team => Dto
                cfg.CreateMap<Team, TeamDto>();
            });
            ;
            app.UseIdentity();
            app.UseMvc();

            if (!env.IsDevelopment()) return;
            app.UseDeveloperExceptionPage();
            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().Migrate(app);
            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().SeedData(app);
            EsportshubContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}