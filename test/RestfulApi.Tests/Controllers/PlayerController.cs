using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.App.Controllers;
using RestfulApi.App.Models.Esportshub.Builders.PlayerBuilders;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Repositories.Players;

namespace RestfulApi.Tests.Controllers
{
    public class PlayerControllerTest : IDisposable
    {
        public PlayerControllerTest()
        {

        }

        [Fact]
         public async void Get_ReturnsAJsonResult_WithAPlayer_IsCorrectType_Test()
         {
            Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();

            IPlayerBuilder playerBuild = Player.Builder();
            playerRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("Hejsa").Build()));
            PlayerController playerController = new PlayerController(playerRepository.Object);
            var player = await playerController.Get(1);
            var contentResult = Assert.IsType<JsonResult>(player);
         }

         [Fact]
         public async void Get_ReturnsAJsonResult_WithAPlayer_WithCorrectInput_Test()
         {
             Mock<IPlayerRepository> _playerRepository = new Mock<IPlayerRepository>();

             IPlayerBuilder playerBuild = Player.Builder();
            _playerRepository.Setup(playerRepository => playerRepository.GetByIdAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("Hejsa").Build()));
             PlayerController playerController = new PlayerController(_playerRepository.Object);
             JsonResult jsonResult = await playerController.Get(1) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.Equal(1, player.PlayerId);
             Assert.Equal("DenLilleMand", player.Nickname);
         }

         public async void Get_ReturnsAJsonResult_WithAPlayer_WithWrongInput_Test()
         {
             Mock<IPlayerRepository> _playerRepository = new Mock<IPlayerRepository>();

             IPlayerBuilder playerBuild = Player.Builder();
            _playerRepository.Setup(playerRepository => playerRepository.GetByIdAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("DenLilleMand").Build()));
             PlayerController playerController = new PlayerController(_playerRepository.Object);
             JsonResult jsonResult = await playerController.Get(-3) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.NotEqual(1, player.PlayerId);
         }

         public async void Get_ReturnsAJsonResult_WithPlayers_WithCorrectInput_Test()
         {
             Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
             IPlayerBuilder playerBuild = Player.Builder();
            playerRepository.Setup(x => x.GetByIdAsync(-3)).Returns(Task.FromResult(playerBuild.SetPlayerId(-3).SetNickname("Hejsa").Build()));
            PlayerController playerController = new PlayerController(playerRepository.Object);
             JsonResult jsonResult = await playerController.Get(2) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.NotEqual(-3, player.PlayerId);
         }

        public void Dispose()
        {
           
        }
    }
}