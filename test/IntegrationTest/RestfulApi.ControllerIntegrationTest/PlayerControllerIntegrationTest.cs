using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.App.Extensions;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            private readonly WebHostBuilder _webHostBuilder;

            public GetPlayersIntegrationTest()
            {

                _webHostBuilder = (WebHostBuilder) new WebHostBuilder().UseStartup<Startup>();
            }

            [Fact]
            public async Task GetPlayers()
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                const string nickname = "Sjuften";
                try
                {
                    var options = new DbContextOptionsBuilder<EsportshubContext>().UseSqlite(connection).Options;
                    using (var context = new EsportshubContext(options))
                    {
                        context.Database.EnsureCreated();
                    }
                    using (var context = new EsportshubContext(options))
                    {
                        _webHostBuilder.ConfigureServices(services => { services.AddScoped(provider => context); });
                        var internalRepository = new InternalRepository<Player>(context);
                        var playerRepo = new PlayerRepository(internalRepository);
                        playerRepo.Insert(Player.Builder().SetNickname(nickname).Build());
                        playerRepo.Save();
                        using (var testServer = new TestServer(_webHostBuilder))
                        {
                            using (var client = testServer.CreateClient())
                            {
                                Console.WriteLine("This is the playerendpoint: {0}, client base address: {1}",
                                    PlayerEndpoint, client.BaseAddress);
                                client.BaseAddress = new Uri("http://localhost:5000");
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
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}