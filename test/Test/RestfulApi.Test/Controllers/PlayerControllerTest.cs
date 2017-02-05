using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App.Controllers;
using RestfulApi.App.Dtos.PlayerDtos;
using Xunit;

namespace Test.RestfulApi.Test.Controllers
{
    public class PlayerControllerTest : IDisposable
    {
        private static readonly Mock<IPlayerRepository> PlayerRepository = new Mock<IPlayerRepository>();
        private static readonly Mock<ILogger<PlayerController>> Logger = new Mock<ILogger<PlayerController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        private static readonly PlayerController PlayerController =
            new PlayerController(PlayerRepository.Object, Logger.Object, Mapper.Object);

        public static List<Mock> Mocks()
        {
            return new List<Mock>
            {
                PlayerRepository,
                Logger,
                Mapper
            };
        }

        public class GetPlayerTest
        {
            private PlayerDto CreatePlayerDto(Guid id, string nickName)
            {
                PlayerDto playerDto = new PlayerDto
                {
                    PlayerGuid = id,
                    Nickname = nickName
                };
                return playerDto;
            }

            [Fact]
            public async void ReturnsJsonResultWhenIdIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                Player player = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                player.PlayerGuid = id;
                PlayerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(player));
                PlayerController playerController = new PlayerController(PlayerRepository.Object,
                    new Logger<PlayerController>(new LoggerFactory()), Mapper.Object);
                Mapper.Setup(x => x.Map<PlayerDto>(player)).Returns(CreatePlayerDto(id, "InsignificantName"));

                var jsonResult = await PlayerController.Get(id);

                Assert.IsType<JsonResult>(jsonResult);
            }

            [Fact]
            public async void ReturnsJsonResultWithValuePlayerDtoWithCorrectValuesWhenCorrectIdTest()
            {
                MockExtensions.ResetAll(Mocks());

                Player player = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                var id = Guid.NewGuid();
                var nickname = "DenLilleMand";
                player.PlayerGuid = id;
                PlayerDto playerDto = CreatePlayerDto(id, nickname);

                Mapper.Setup(mapper => mapper.Map<PlayerDto>(player)).Returns(playerDto);
                PlayerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(player));
                JsonResult jsonResult = await PlayerController.Get(id) as JsonResult;
                Assert.NotNull(jsonResult);
                PlayerDto playerDtoResult = jsonResult.Value as PlayerDto;

                Assert.NotNull(playerDtoResult);
                Assert.True(id == playerDtoResult.PlayerGuid);
                Assert.Equal("DenLilleMand", playerDtoResult.Nickname);
            }

            [Fact]
            public async void ReturnsBadRequestObjectResultWhenIdIsInvalidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.Empty;

                PlayerRepository.Setup(x => x.FindAsync(id)).Throws<Exception>();
                var playerController = new PlayerController(PlayerRepository.Object,
                    new Logger<PlayerController>(new LoggerFactory()), Mapper.Object);
                PlayerRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await PlayerController.Get(id) as BadRequestObjectResult;

                Assert.NotNull(result);
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }


        public class CreatePlayerTest
        {
        }

        public class DeletePlayerTest
        {
        }

        public class UpdatePlayerTest
        {
        }

        public class GetPlayersTest
        {
            private static IEnumerable<Player> GetPlayers(IEnumerable<Guid> playerIds)
            {
                IEnumerable<Player> players = new List<Player>();
                foreach (var playerId in playerIds)
                {
                    var player = (Player) Activator.CreateInstance(typeof(Player), true);
                    player.PlayerGuid = playerId;
                    players.Append(player);
                }
                return players;
            }

            [Fact]
            public async void ReturnsJsonResultWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var playerIds = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
                var players = GetPlayers(playerIds);

                PlayerRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(players);
                foreach (var playerId in playerIds)
                {
                    var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                    instance.PlayerGuid = playerId;
                    var playerDto = new PlayerDto {PlayerGuid = playerId};
                    Mapper.Setup(x => x.Map<PlayerDto>(instance)).Returns(playerDto);
                }

                var playerDtos = await PlayerController.Get() as JsonResult;
                Assert.IsType<JsonResult>(playerDtos);
            }

            [Fact]
            public async void ReturnsJsonResultWithIEnumerablePlayerDtoAsValueWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var playerIds = new[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
                var players = GetPlayers(playerIds);

                PlayerRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(players);
                foreach (var playerId in playerIds)
                {
                    var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                    instance.PlayerGuid = playerId;
                    var playerDto = new PlayerDto {PlayerGuid = playerId};
                    Mapper.Setup(x => x.Map<PlayerDto>(instance)).Returns(playerDto);
                }

                var result = await PlayerController.Get() as JsonResult;
                Assert.NotNull(result);
                var playerDtos = result.Value as IEnumerable<PlayerDto>;

                Assert.NotNull(playerDtos);
                foreach (var playerDto in playerDtos)
                {
                    Assert.True(playerIds.Contains(playerDto.PlayerGuid));
                }
            }

            [Fact]
            public async void ReturnsNotFoundResultWhenItDoesntFindAnythingTest()
            {
                MockExtensions.ResetAll(Mocks());

                PlayerRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Player, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(null);

                var result = await PlayerController.Get() as NotFoundResult;

                Assert.IsType<NotFoundResult>(result);
            }
        }

        public void Dispose()
        {
        }
    }
}