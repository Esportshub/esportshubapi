using Xunit;

namespace Data.Test.DbContext
{
    public class PlayerDbSetTest
    {
        [Fact]
        public void AddPlayer()
        {
            var options = new DbContextOptionsBuilder<EsportshubContext>()
                .UseInMemoryDatabase("AddedPlayer")
                .Options;

            var player = Player.Builder().SetNickname("Sjuften").Build();
            using (var context = new EsportshubContext(options))
            {
                var service = new PlayerRepository(context);
                service.Insert(player);
                service.Save();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new EsportshubContext(options))
            {
                Assert.Equal(1, context.Players.ToList().Count());
                Assert.Equal(player.Nickname, context.Players.Single().Nickname);
            }
        }
    }
}