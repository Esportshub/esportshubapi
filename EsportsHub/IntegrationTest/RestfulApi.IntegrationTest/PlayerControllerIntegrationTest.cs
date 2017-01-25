using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Activities;
using Data.App.Models.Repositories.Events;
using Data.App.Models.Repositories.Games;
using Data.App.Models.Repositories.Groups;
using Data.App.Models.Repositories.Integrations;
using Data.App.Models.Repositories.Players;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
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
            private static readonly Mock<IPlayerRepository> PlayerRepository = new Mock<IPlayerRepository>();
            private static readonly Mock<IGameRepository> GameRepository = new Mock<IGameRepository>();
            private static readonly Mock<IGroupRepository> GroupRepository = new Mock<IGroupRepository>();
            private static readonly Mock<IActivityRepository> ActivityRepository = new Mock<IActivityRepository>();
            private static readonly Mock<IEventRepository> EventRepository = new Mock<IEventRepository>();
            private static readonly Mock<ITeamRepository> TeamRepository = new Mock<ITeamRepository>();
            private static readonly Mock<IIntegrationRepository> IntegrationRepository = new Mock<IIntegrationRepository>();
            private static readonly Mock<EsportshubContext> DbContextMock = new Mock<EsportshubContext>();

            private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
            private static readonly Mock<ILogger> Logger = new Mock<ILogger>();

            [Theory]
            [InlineData("?playerIds=1,2,3,4,5")]
            [InlineData("?playerIds=3,5,9,13,700")]
            public async Task GetPlayersWithCorrectIds(string inputQueryString)
            {
                //setup - mapper
                Mapper.Setup(x => x.Map<PlayerDto>(It.IsAny<Player>())).Returns(new PlayerDto());

                //setup - what players we expect to get returned with this queryString
                var splittedQueryResult = inputQueryString.Split(new char[] {'='});
                var queryStringValues = splittedQueryResult[1].Split(new char[] {','}).Select(int.Parse);
                var players = GetPlayers(queryStringValues);

                //setup - Simply trying to understand what the queryString class does in C# and how it might actually parse our real queryString
                //        was mainly created and not used because i want to know it parses our queryString like playerIds=1,2,3,4,5 like a Enumerable<int>
                QueryString queryString = new QueryString(inputQueryString);
                StringValues stringValues = new StringValues(new[] {"1", "2", "3", "4", "5"});
                var dictionary = new Dictionary<string, StringValues>();
                dictionary.Add("playerIds", stringValues);
                var queryCollection = new QueryCollection(dictionary);
                var queryStringValueProvider = new QueryStringValueProvider(BindingSource.Query, queryCollection, CultureInfo.CurrentCulture);
                ValueProviderResult ids = queryStringValueProvider.GetValue("playerIds");

                //setup - setting up the repository to return the players matching our queryString as we expect
                PlayerRepository.Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(players));

                //setup - setting up the server with mocked versions of the services we use from it
                var webHostBuilder = new WebHostBuilder().UseStartup<Startup>().ConfigureServices(services =>
                {
                    services.AddScoped<IPlayerRepository>(provider => PlayerRepository.Object);
                    services.AddScoped<IGameRepository>(provider => GameRepository.Object);
                    services.AddScoped<IGroupRepository>(provider => GroupRepository.Object);
                    services.AddScoped<ITeamRepository>(provider => TeamRepository.Object);
                    services.AddScoped<IIntegrationRepository>(provider => IntegrationRepository.Object);
                    services.AddScoped<IActivityRepository>(provider => ActivityRepository.Object);
                    services.AddScoped<IGroupRepository>(provider => GroupRepository.Object);
                    services.AddScoped<IEventRepository>(provider => EventRepository.Object);
                    services.AddScoped<EsportshubContext>(provider => DbContextMock.Object);
                    services.AddScoped<ILogger>(provider => Logger.Object);
                    services.AddScoped<IMapper>(provider => Mapper.Object);
                });
                webHostBuilder.CaptureStartupErrors(true);
                using (var server = new TestServer(webHostBuilder))
                {
                    using (var client = server.CreateClient())
                    {
                        //act
                        var response = await client.GetAsync(PlayerEndpoint + inputQueryString);
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