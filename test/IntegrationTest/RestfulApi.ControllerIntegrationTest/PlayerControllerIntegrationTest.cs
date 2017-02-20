using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
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
            private readonly DbContextOptions<EsportshubContext> _options;
            private readonly WebHostBuilder _webHostBuilder;

            public GetPlayersIntegrationTest()
            {
                _options = new DbContextOptionsBuilder<EsportshubContext>()
                    .UseInMemoryDatabase(databaseName: "memory")
                    .Options;

                _webHostBuilder = (WebHostBuilder) new WebHostBuilder().UseStartup<Startup>();
            }

            [Fact]
            public void GetPlayers()
            {
                const string nickname = "Sjuften";

                using (var context = new EsportshubContext(_options))
                {
                    _webHostBuilder.ConfigureServices(services => { services.TryAddScoped(provider => context); });
                    var inter = new InternalRepository<Player>(context);
                    var playerRepo = new PlayerRepository(inter);
                    playerRepo.Insert(Player.Builder().SetNickname(nickname).Build());
                    playerRepo.Save();
                    using (var testServer = new TestServer(_webHostBuilder))
                    {
                        using (var client = testServer.CreateClient())
                        {
                            Task<HttpResponseMessage> message = client.GetAsync(PlayerEndpoint);
                            message.Wait();
                            Task<string> answer = message.Result.Content.ReadAsStringAsync();
                            answer.Wait();
                            Console.WriteLine(message.Result.Content.ReadAsStringAsync().Result + "HEEEEEEEEEey ");
                            Console.WriteLine(answer.Result + "----------");
                            var final = answer.Result;
                            var playerDtos = JsonConvert.DeserializeObject<List<PlayerDto>>(final);
                            //assert
                            Assert.Equal(nickname, playerDtos.FirstOrDefault().Nickname);
                        }
                    }
                }
            }
        }
    }
}