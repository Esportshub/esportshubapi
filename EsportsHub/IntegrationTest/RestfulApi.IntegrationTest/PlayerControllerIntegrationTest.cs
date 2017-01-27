using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Activities;
using Data.App.Models.Repositories.Events;
using Data.App.Models.Repositories.Games;
using Data.App.Models.Repositories.Groups;
using Data.App.Models.Repositories.Integrations;
using Data.App.Models.Repositories.Players;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App;
using RestfulApi.App.Dtos.PlayerDtos;
using Xunit;

namespace IntegrationTest.RestfulApi.IntegrationTest
{
    public class PlayerControllerIntegrationTest
    {
        protected readonly string PlayerEndpoint = "/api/players";

        private IEnumerable<Player> GetPlayers(IEnumerable<int> playerIds)
        {
            IEnumerable<Player> players = new List<Player>();
            foreach (var playerId in playerIds)
            {
                var player = (Player) Activator.CreateInstance(typeof(Player), true);
                player.PlayerId = playerId;
                players.Append(player);
            }
            return players;
        }

        public class GetPlayersIntegrationTest : PlayerControllerIntegrationTest
        {
            private static readonly Mock<IGameRepository> GameRepository = new Mock<IGameRepository>();
            private static readonly Mock<IGroupRepository> GroupRepository = new Mock<IGroupRepository>();
            private static readonly Mock<IActivityRepository> ActivityRepository = new Mock<IActivityRepository>();
            private static readonly Mock<IEventRepository> EventRepository = new Mock<IEventRepository>();
            private static readonly Mock<ITeamRepository> TeamRepository = new Mock<ITeamRepository>();
            private static readonly Mock<IIntegrationRepository> IntegrationRepository = new Mock<IIntegrationRepository>();
            private static readonly Mock<EsportshubContext> DbContextMock = new Mock<EsportshubContext>();
            private static readonly Mock<IPlayerRepository> PlayerRepository = new Mock<IPlayerRepository> { CallBase = true};
            private static readonly Mock<IRepository<Player>> InternalPlayerRepository = new Mock<IRepository<Player>>();
            private static readonly Mock<IRepository<Team>> InternalTeamRepository = new Mock<IRepository<Team>>();

            private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
            private static readonly Mock<ILogger> Logger = new Mock<ILogger>();

            [Theory]
            [InlineData("?filter[ids]=1,2,3,4,5")]
            public async Task GetPlayersWithCorrectIds(string inputQueryString)
            {
                Mapper.Setup(x => x.Map<PlayerDto>(It.IsAny<Player>())).Returns(new PlayerDto());
                var splittedQueryResult = inputQueryString.Split(new char[] {'='});
                Console.WriteLine(splittedQueryResult);
                int herpderp;
                var queryStringValues = splittedQueryResult[1].Split(new char[] {','}).Select(id =>
                        {
                            int parsedId;
                            bool success = int.TryParse(id, out parsedId);
                            return new {success, parsedId};
                        }).Where(pair => pair.success).Select(pair => pair.parsedId);

                var players = GetPlayers(queryStringValues);
                PlayerRepository.Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(players));
                InternalPlayerRepository.Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(players));
                var webHostBuilder = new WebHostBuilder().UseStartup<Startup>().ConfigureServices(services =>
                {
                    services.AddScoped<IRepository<Player>, GenericRepository<Player>>();
                    services.AddScoped<IRepository<Group>, GenericRepository<Group>>();
                    services.AddScoped<IRepository<Game>, GenericRepository<Game>>();
                    services.AddScoped<IRepository<Activity>, GenericRepository<Activity>>();
                    services.AddScoped<IRepository<Team>, GenericRepository<Team>>();
                    services.AddScoped<IRepository<Integration>, GenericRepository<Integration>>();
                    services.AddScoped<IRepository<Event>, GenericRepository<Event>>();

                    //herpderp
                    services.AddScoped<IRepository<Player>>(provider => InternalPlayerRepository.Object);
                    services.AddScoped<IRepository<Team>>(provider => InternalTeamRepository.Object);
                    services.AddScoped<IRepository<Group>, GenericRepository<Group>>();
                    services.AddScoped<IRepository<Game>, GenericRepository<Game>>();
                    services.AddScoped<IRepository<Activity>, GenericRepository<Activity>>();
                    services.AddScoped<IRepository<Integration>, GenericRepository<Integration>>();
                    services.AddScoped<IRepository<Event>, GenericRepository<Event>>();

                    services.AddScoped(provider => PlayerRepository.Object);
                    services.AddScoped(provider => GameRepository.Object);
                    services.AddScoped(provider => GroupRepository.Object);
                    services.AddScoped(provider => IntegrationRepository.Object);
                    services.AddScoped(provider => TeamRepository.Object);
                    services.AddScoped(provider => ActivityRepository.Object);
                    services.AddScoped(provider => GroupRepository.Object);
                    services.AddScoped(provider => EventRepository.Object);
                    services.AddScoped(provider => DbContextMock.Object);
                    services.AddScoped(provider => Logger.Object);
                    services.AddScoped(provider => Mapper.Object);
                });
                webHostBuilder.CaptureStartupErrors(true);
                using (var server = new TestServer(webHostBuilder))
                {
                    using (var client = server.CreateClient())
                    {
                        //act
                        var response = await client.GetAsync(PlayerEndpoint);
                        Console.WriteLine("Response from the server:");
                        Console.WriteLine(response);
                        response.EnsureSuccessStatusCode();

                        //assert
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    }
                }
            }
        }
    }
}