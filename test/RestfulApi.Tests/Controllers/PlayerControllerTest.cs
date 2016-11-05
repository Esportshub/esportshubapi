using System;
using Xunit;
using Moq;
using EsportshubApi.Models.Repositories;
using EsportshubApi.Models;
using EsportshubApi.Models.Entities;
using EsportshubApi.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestfulApi.Tests.Controllers
{
    public class PlayerControllerTest : IDisposable
    {
        private PlayerController _playerController;

        public PlayerControllerTest()
        {
            Mock<IPlayerRepository> _playerRepository = new Mock<IPlayerRepository>();
            IPlayerBuilder playerBuild = new PlayerBuilder();
            _playerRepository.Setup(playerRepository => playerRepository.GetByIdAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("DenLilleMand").Build()));
            _playerRepository.Setup(playerRepository => playerRepository.GetByIdAsync(-3)).Returns(Task.FromResult(playerBuild.SetPlayerId(-3).SetNickname("Hejsa").Build()));
            _playerController = new PlayerController(_playerRepository.Object);
        }

        [Fact]
         public async void Get_ReturnsAJsonResult_WithAPlayer_IsCorrectType_Test()
         {
             var player = await _playerController.Get(1);
             var contentResult = Assert.IsType<JsonResult>(player);
         }

         [Fact]
         public async void Get_ReturnsAJsonResult_WithAPlayer_WithCorrectInput_Test()
         {
             JsonResult jsonResult = await _playerController.Get(1) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.Equal(1, player.PlayerId);
             Assert.Equal("DenLilleMand", player.Nickname);
         }

         public async void Get_ReturnsAJsonResult_WithAPlayer_WithWrongInput_Test()
         {
             JsonResult jsonResult = await _playerController.Get(-3) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.Equal(-3, player.PlayerId);
         }

         public async void Get_ReturnsAJsonResult_WithPlayers_WithCorrectInput_Test()
         {
             JsonResult jsonResult = await _playerController.Get(2) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.NotEqual(-3, player.PlayerId);
         }

        public void Dispose()
        {
            _playerController = null;
        }
    }
}