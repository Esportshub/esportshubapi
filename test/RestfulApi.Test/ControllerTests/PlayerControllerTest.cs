using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace RestfullApi.Test.ControllerTests
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
            playerRepository.Setup(x => x.FindAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("Hejsa").Build()));
            PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
            var player = await playerController.Get(1);
            var contentResult = Assert.IsType<JsonResult>(player);
         }

         [Fact]
         public async void Get_ReturnsAJsonResult_WithAPlayer_WithCorrectInput_Test()
         {
             Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();

             IPlayerBuilder playerBuild = Player.Builder();
             playerRepository.Setup(x => x.FindAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("Hejsa").Build()));
             PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
             JsonResult jsonResult = await playerController.Get(1) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.Equal(1, player.PlayerId);
             Assert.Equal("DenLilleMand", player.Nickname);
         }

         public async void Get_ReturnsAJsonResult_WithAPlayer_WithWrongInput_Test()
         {
             Mock<IPlayerRepository> _playerRepository = new Mock<IPlayerRepository>();

             IPlayerBuilder playerBuild = Player.Builder();
            _playerRepository.Setup(playerRepository => playerRepository.FindAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("DenLilleMand").Build()));
             PlayerController playerController = new PlayerController(_playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
             JsonResult jsonResult = await playerController.Get(-3) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.NotEqual(1, player.PlayerId);
         }

         public async void Get_ReturnsAJsonResult_WithPlayers_WithCorrectInput_Test()
         {
             Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
             IPlayerBuilder playerBuild = Player.Builder();
             playerRepository.Setup(x => x.FindAsync(-3)).Returns(Task.FromResult(playerBuild.SetPlayerId(-3).SetNickname("Hejsa").Build()));
             PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()));
             JsonResult jsonResult = await playerController.Get(2) as JsonResult;
             Player player = jsonResult.Value as Player;
             Assert.NotEqual(-3, player.PlayerId);
         }

        public void Dispose()
        {
           
        }
    }
}