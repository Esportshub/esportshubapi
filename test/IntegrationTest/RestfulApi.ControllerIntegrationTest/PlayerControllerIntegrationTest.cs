using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using RestfulApi.App;
using RestfulApi.App.Dtos.PlayerDtos;
using Xunit;

namespace IntegrationTest.RestfulApi.ControllerIntegrationTest
{
    public class PlayerControllerIntegrationTest
    {
        protected static readonly string PlayerEndpoint = "/api/players";

        public class GetPlayersIntegrationTest
        {
            private readonly DbContextOptions _options;
            private readonly WebHostBuilder _webHostBuilder;

            public GetPlayersIntegrationTest()
            {
                _options = new DbContextOptionsBuilder<EsportshubContext>()
                    .UseInMemoryDatabase(databaseName: "memory")
                    .Options;

                _webHostBuilder = (WebHostBuilder) new WebHostBuilder().UseStartup<Startup>();
            }

            [Fact]
            public async Task GetPlayers()
            {
                const string nickname = "Sjuften";

                _webHostBuilder.ConfigureServices(services => { services.TryAddScoped(provider => new EsportshubContext(_options)); });
                using (var testServer = new TestServer(_webHostBuilder))
                {
                    using (var context = new EsportshubContext(_options))
                    {
                        var inter = new InternalRepository<Player>(context);
                        var playerRepo = new PlayerRepository(inter);
                        playerRepo.Insert(Player.Builder().SetNickname(nickname).Build());
                        playerRepo.Save();

                        using (var client = testServer.CreateClient())
                        {
                            var response = await client.GetAsync(PlayerEndpoint);
                            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                            var playerDtos =
                                JsonConvert.DeserializeObject<List<PlayerDto>>(response.Content.ReadAsStringAsync()
                                    .Result);
                            //assert
                            Assert.Equal(nickname, playerDtos.FirstOrDefault().Nickname);
                        }
                    }
                }
            }
        }
    }
}