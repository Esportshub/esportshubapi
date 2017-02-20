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

        public class GetPlayersIntegrationTest : IDisposable
        {
            private readonly SqliteConnection _connection;
            private readonly WebHostBuilder _webHostBuilder;
            private readonly EsportshubContext _context;
            private HttpClient _client;

            public GetPlayersIntegrationTest()
            {
                _context = new EsportshubContext(new DbContextOptionsBuilder<EsportshubContext>()
                    .UseSqlite(_connection = new SqliteConnection("DataSource=:memory:"))
                    .Options);

                _webHostBuilder = (WebHostBuilder) new WebHostBuilder().UseStartup<Startup>()
                    .ConfigureServices(
                        services => { services.AddScoped(provider => _context); });
            }
            [Fact]
            public async Task GetPlayers()
            {
                const string nickname = "Sjuften";

                _connection.Open();
                _context.Database.EnsureCreated();

                var repo = new PlayerRepository(new InternalRepository<Player>(_context));

                repo.Insert(Player.Builder().SetNickname(nickname).Build());
                repo.Save();

                _client = new TestServer(_webHostBuilder).CreateClient();
                var result = await _client.GetAsync(PlayerEndpoint).Result.Content.ReadAsStringAsync();
                var playerDtos = JsonConvert.DeserializeObject<List<PlayerDto>>(result);





                //assert
                Assert.Equal(nickname, playerDtos.FirstOrDefault().Nickname);
            }

            public void Dispose()
            {
                _connection.Dispose();
                _client.Dispose();
                _context.Dispose();
                GC.SuppressFinalize(this);
                Console.WriteLine("Disposing context");
            }
        }
    }
}