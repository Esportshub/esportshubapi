using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

            [Fact]
            public async void ReturnsJsonResultWhenIdIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());

                var id = Guid.NewGuid();
                var player = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                player.PlayerGuid = id;

                var playerDto = new PlayerDto() { PlayerGuid = id};

                PlayerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(player));
                Mapper.Setup(x => x.Map<PlayerDto>(player)).Returns(playerDto);

                var jsonResult = await PlayerController.Get(id);

                Assert.IsType<JsonResult>(jsonResult);
            }

            [Fact]
            public async void ReturnsJsonResultWithValuePlayerDtoWithCorrectValuesWhenCorrectIdTest()
            {
                MockExtensions.ResetAll(Mocks());

                var player = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                var id = Guid.NewGuid();
                const string nickname = "DenLilleMand";
                player.PlayerGuid = id;
                var playerDto = new PlayerDto() { PlayerGuid = id, Nickname = nickname};

                Mapper.Setup(mapper => mapper.Map<PlayerDto>(player)).Returns(playerDto);
                PlayerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(player));
                var jsonResult = await PlayerController.Get(id) as JsonResult;
                Assert.NotNull(jsonResult);
                var playerDtoResult = jsonResult.Value as PlayerDto;

                Assert.NotNull(playerDtoResult);
                Assert.True(id == playerDtoResult.PlayerGuid);
                Assert.Equal(nickname, playerDtoResult.Nickname);
            }

            [Fact]
            public async void ReturnsBadRequestResultWhenIdIsInvalidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.Empty;

                PlayerRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await PlayerController.Get(id) as BadRequestResult;

                Assert.NotNull(result);
                Assert.IsType<BadRequestResult>(result);
            }
        }


        public class CreatePlayerTest
        {
            [Fact]
            public async void ReturnsCreateAtRouteResultIfDataIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                instance.PlayerGuid = id;
                var playerDto = new PlayerDto {PlayerGuid = id};

                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                PlayerRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Player>(playerDto)).Returns(instance);
                Mapper.Setup(m => m.Map<PlayerDto>(instance)).Returns(playerDto);

                var result = await PlayerController.Create(playerDto);
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfValidDataIsNotSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                PlayerRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Player>(It.IsAny<PlayerDto>())).Returns(instance);

                var result = await PlayerController.Create(new PlayerDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsBadRequestTypeIfPlayerDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await PlayerController.Create(null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void IfCreatedAtRouteResultIsCreatedWithCorrectValuesWhenAValidPlayerIsCreatedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var id = Guid.NewGuid();
                var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                instance.PlayerGuid = id;
                var playerDto = new PlayerDto() { PlayerGuid = id};

                PlayerRepository.Setup(x => x.Insert(instance));
                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                Mapper.Setup(m => m.Map<Player>(playerDto)).Returns(instance);
                Mapper.Setup(m => m.Map<PlayerDto>(instance)).Returns(playerDto);

                var result = await PlayerController.Create(playerDto) as CreatedAtRouteResult;
                Assert.NotNull(result);
                Assert.Equal(id, result.RouteValues["Id"]);
            }

            [Fact]
            public async void IfCreatedAtRouteResultIsPlayerWhenAValidPlayerIsSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var id = Guid.NewGuid();
                var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                instance.PlayerGuid = id;
                var playerDto = new PlayerDto() { PlayerGuid = id};

                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                PlayerRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Player>(playerDto)).Returns(instance);
                Mapper.Setup(m => m.Map<PlayerDto>(instance)).Returns(playerDto);

                var result = await PlayerController.Create(playerDto) as CreatedAtRouteResult;

                Assert.NotNull(result);
                var routeObject = result.Value as PlayerDto;
                Assert.IsType<PlayerDto>(routeObject);
            }
        }

        public class DeletePlayerTest
        {
            [Fact]
            public async void ReturnsNotFoundResultIfPlayerDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                PlayerRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await PlayerController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfPlayerIsDeletedWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                PlayerRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                PlayerRepository.Setup(x => x.Delete(id));
                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                Mapper.Setup(x => x.Map<Player>(It.IsAny<PlayerDto>())).Returns(instance);


                var result = await PlayerController.Delete(id);
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async void ReturnsStatusCodeResultWithStatusCode500IfDataIsNotDeletedWithValidPlayerGuidTest()
            {
                MockExtensions.ResetAll(Mocks());

                var id = Guid.NewGuid();
                var instance = (Player)Activator.CreateInstance(typeof(Player), nonPublic: true);
                instance.PlayerGuid = id;

                PlayerRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                PlayerRepository.Setup(x => x.Delete(id));
                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await PlayerController.Delete(id) as ObjectResult;
                Assert.NotNull(result);
                Assert.IsType<ObjectResult>(result);
                Assert.Equal(result.StatusCode, (int)HttpStatusCode.InternalServerError);
            }
        }

        public class UpdatePlayerTest
        {
            [Fact]
            public async void ReturnsBadRequestResultTypeIfPlayerDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await PlayerController.Update(new Guid(), null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsBadRequestResultIfIdIsEmptyAndPlayerDtoIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var result = await PlayerController.Update(Guid.Empty, new PlayerDto() { PlayerGuid = id});

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsBadRequestResultIfIdIsEmptyAndPlayerDtoIsValidButEmptyGuidTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await PlayerController.Update(Guid.Empty, new PlayerDto());

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultWithStatusCode500IfDataIsNotUpdatedWithValidPlayerDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                PlayerRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                PlayerRepository.Setup(x => x.Update(instance));
                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await PlayerController.Update(id, new PlayerDto() {PlayerGuid = id});
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfPlayerIsUpdatedWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                PlayerRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                PlayerRepository.Setup(x => x.Update(instance));
                PlayerRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await PlayerController.Update(id, new PlayerDto() {PlayerGuid = id});
                Assert.IsType<OkObjectResult>(result);
            }
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
    }
}