using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Data.App.Models.Builders.PlayerBuilders;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;
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
        public PlayerControllerTest()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Player, PlayerDto>().ReverseMap();
                cfg.CreateMap<PlayerGames, PlayerGamesDto>().ReverseMap();
                cfg.CreateMap<PlayerGroups, PlayerGroupsDto>().ReverseMap();
                cfg.CreateMap<PlayerTeams, PlayerTeamsDto>().ReverseMap();
            });
        }

        public class GetALlPlayersTest
        {


        }

        public class GetPlayerTest
        {
             [Fact]
             public async void ReturnCorrectType()
             {
                Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
                IPlayerBuilder playerBuild = Player.Builder();

                playerRepository.Setup(x => x.FindAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("Hejsa").Build()));
                PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
                var player = await playerController.Get(1);

                Assert.IsType<JsonResult>(player);
             }

             [Fact]
             public async void IdTest()
             {
                 Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
                 IPlayerBuilder playerBuild = Player.Builder();

                 playerRepository.Setup(x => x.FindAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("Hejsa").Build()));
                 PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
                 JsonResult jsonResult = await playerController.Get(1) as JsonResult;
                 Assert.NotNull(jsonResult);
                 PlayerDto playerDto = jsonResult.Value as PlayerDto;

                 Assert.NotNull(playerDto );
                 Assert.Equal(1, playerDto.PlayerId);
                 Assert.Equal("DenLilleMand", playerDto.Nickname);
             }

             [Fact]
             public async void BelowZeroId()
             {
                 Mock<IPlayerRepository> _playerRepository = new Mock<IPlayerRepository>();
                 IPlayerBuilder playerBuild = Player.Builder();

                 _playerRepository.Setup(playerRepository => playerRepository.FindAsync(-3))
                     .Throws<Exception>();
                 PlayerController playerController = new PlayerController(_playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
                 JsonResult jsonResult = await playerController.Get(-3) as JsonResult;
             }

             [Fact]
             public async void Get_ReturnsAJsonResult_WithPlayers_WithCorrectInput_Test()
             {
                 Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
                 IPlayerBuilder playerBuild = Player.Builder();
                 playerRepository.Setup(x => x.FindAsync(-3)).Returns(Task.FromResult(playerBuild.SetPlayerId(-3).SetNickname("Hejsa").Build()));
                 PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
                 JsonResult jsonResult = await playerController.Get(2) as JsonResult;
                 PlayerDto player = jsonResult.Value as PlayerDto;
                 Assert.NotEqual(-3, player.PlayerId);
             }
        }

        public class CreatePlayerTest
        {


        }




        public void Dispose()
        {
           
        }
    }
}
