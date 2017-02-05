using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Players;
using Moq;
using Xunit;

namespace Test.RestfulApi.Test.Repositories
{
    public class PlayerRepositoryTest
    {
        public class FindByAsyncTest
        {
            private readonly Mock<IRepository<Player>> _internalPlayerRepository = new Mock<IRepository<Player>>();
            private readonly Mock<EsportshubContext> _esportshubContext = new Mock<EsportshubContext>();


            private List<Player> GetPlayers(IEnumerable<Guid> playerIds)
            {
                List<Player> players = new List<Player>();
                foreach (var playerId in playerIds)
                {
                    var player = (Player) Activator.CreateInstance(typeof(Player), true);
                    player.PlayerGuid = playerId;
                    players.Add(player);
                }
                return players;
            }

<<<<<<< HEAD
            [Theory]
            [InlineData(new[] {1, 2, 3, 4})]
            [InlineData(new[] {500, 700, 750, 9999})]
            [InlineData(new[] {50000, 10000, 90000, 150000})]
            public async void FindsAsyncReturnsTheCorrectAmountOfPlayers(int[] ids)
            {
                var players = GetPlayers(ids);
                _internalPlayerRepository
                    .Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(players);
                IPlayerRepository playerRepository =
                    new PlayerRepository(_esportshubContext.Object, _internalPlayerRepository.Object);
                var result = await playerRepository.FindByAsync(player => ids.Contains(player.PlayerId), "");
=======
            [Fact]
            public async void FindsAsyncReturnsTheCorrectAmountOfPlayers()
            {
                var playerIds = new[] {new Guid(), new Guid(), new Guid(), new Guid()};
                var players = GetPlayers(playerIds);
                _internalPlayerRepository.Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>())).ReturnsAsync(players);
                IPlayerRepository playerRepository = new PlayerRepository(_esportshubContext.Object, _internalPlayerRepository.Object);
                var result = await playerRepository.FindByAsync(player => playerIds.Contains(player.PlayerGuid), "");
>>>>>>> 81770255d15633fda09b7e992f52b76c277f82fb
                Assert.NotNull(result);
                Assert.IsType<List<Player>>(result);
                Assert.Equal(4, result.ToList().Count);
            }
        }

        public class FindAsyncTest
        {
        }

        public class SaveAsyncTest
        {
        }

        public class InsertTest
        {
        }

        public class DeleteTest
        {
        }

        public class UpdateTest
        {
        }

        public class SaveTest
        {
        }
    }
}