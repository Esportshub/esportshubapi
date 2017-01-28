using AutoMapper;
using Data.App.Extensions;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;
using Data.App.Models.Entities.Mappings;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Activities;
using Data.App.Models.Repositories.Events;
using Data.App.Models.Repositories.Games;
using Data.App.Models.Repositories.Groups;
using Data.App.Models.Repositories.Integrations;
using Data.App.Models.Repositories.Players;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            services.AddMvc();
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Activity, ActivityDto>().ReverseMap();
                cfg.CreateMap<EsportshubEvent, EsportshubEventDto>().ReverseMap();
                cfg.CreateMap<Game, GameDto>().ReverseMap();
                cfg.CreateMap<Group, GroupDto>().ReverseMap();
                cfg.CreateMap<Integration, IntegrationDto>().ReverseMap();
                cfg.CreateMap<Player, PlayerDto>().ReverseMap();
                cfg.CreateMap<PlayerGames, PlayerGamesDto>().ReverseMap();
                cfg.CreateMap<PlayerGroups, PlayerGroupsDto>().ReverseMap();
                cfg.CreateMap<PlayerTeams, PlayerTeamsDto>().ReverseMap();
                cfg.CreateMap<SocialMedia, SocialMediaDto>().ReverseMap();
                cfg.CreateMap<Team, TeamDto>().ReverseMap();
            });
            services.AddDbContext<EsportshubContext>();

            services.TryAddScoped<IRepository<Player>, GenericRepository<Player>>();
            services.TryAddScoped<IRepository<Group>, GenericRepository<Group>>();
            services.TryAddScoped<IRepository<Game>, GenericRepository<Game>>();
            services.TryAddScoped<IRepository<Activity>, GenericRepository<Activity>>();
            services.TryAddScoped<IRepository<Team>, GenericRepository<Team>>();
            services.TryAddScoped<IRepository<Integration>, GenericRepository<Integration>>();
            services.TryAddScoped<IRepository<EsportshubEvent>, GenericRepository<EsportshubEvent>>();

            services.TryAddScoped<IPlayerRepository, PlayerRepository>();
            services.TryAddScoped<IGameRepository, GameRepository>();
            services.TryAddScoped<IGroupRepository, GroupRepository>();
            services.TryAddScoped<IActivityRepository, ActivityRepository>();
            services.TryAddScoped<IEventRepository, EventRepository>();
            services.TryAddScoped<IIntegrationRepository, IntegrationRepository>();
            services.TryAddScoped<ITeamRepository, TeamRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseMvc();

            if (!env.IsDevelopment()) return;
            app.UseDeveloperExceptionPage();
            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().Migrate(app);
            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().SeedData(app);
        }
    }
}