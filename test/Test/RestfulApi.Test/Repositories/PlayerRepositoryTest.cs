using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
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

            [Theory]
            [InlineData(new int[]{1,2,3,4})]
            [InlineData(new int[]{500,700,750,9999})]
            [InlineData(new int[]{50000,10000,90000,150000})]
            public async void FindsAsyncReturnsTheCorrectAmountOfPlayers(int[] ids)
            {

                var players = GetPlayers(ids);
                _internalPlayerRepository.Setup(x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>())).ReturnsAsync(players);
                IPlayerRepository playerRepository = new PlayerRepository(_esportshubContext.Object, _internalPlayerRepository.Object);

                var result = await playerRepository.FindByAsync(player => ids.Contains(player.PlayerId), "");
                Assert.NotNull(result);
                Assert.IsType<IEnumerable<Player>>(result);
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