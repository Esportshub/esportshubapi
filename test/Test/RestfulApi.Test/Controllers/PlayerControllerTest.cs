using System;
using System.Threading.Tasks;
using AutoMapper;
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

        public class GetPlayerTest
        {
            private readonly IMapper _mapper;

            public GetPlayerTest()
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Player, PlayerDto>().ReverseMap();
                    cfg.CreateMap<PlayerGames, PlayerGamesDto>().ReverseMap();
                    cfg.CreateMap<PlayerGroups, PlayerGroupsDto>().ReverseMap();
                    cfg.CreateMap<PlayerTeams, PlayerTeamsDto>().ReverseMap();
                });
                _mapper = config.CreateMapper();
            }

            [Theory]
            [InlineData("1")]
            [InlineData("37")]
            [InlineData("50000")]
            [InlineData("100000")]
            public async void ReturnCorrectType(string inputId)
            {
                Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
                IPlayerBuilder playerBuild = Player.Builder();
                int id = int.Parse(inputId);

                playerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(playerBuild.SetPlayerId(id).SetNickname("Hejsa").Build()));
                PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()), _mapper);
                var player = await playerController.Get(inputId);

                Assert.IsType<JsonResult>(player);
            }

            [Theory]
            [InlineData("1")]
            [InlineData("37")]
            [InlineData("50000")]
            [InlineData("100000")]
            public async void CorrectIdTest(string inputId)
            {
                Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
                int id = int.Parse(inputId);

                IPlayerBuilder playerBuild = Player.Builder();

                playerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(playerBuild.SetPlayerId(id).SetNickname("DenLilleMand").Build()));
                PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()), _mapper);
                JsonResult jsonResult = await playerController.Get(inputId) as JsonResult;
                Assert.NotNull(jsonResult);
                PlayerDto playerDto = jsonResult.Value as PlayerDto;

                Assert.NotNull(playerDto);
                Assert.Equal(id, playerDto.PlayerId);
                Assert.Equal("DenLilleMand", playerDto.Nickname);
            }

            [Theory]
            [InlineData("0")]
            [InlineData("-1")]
            [InlineData("-50")]
            [InlineData("-100000")]
            public async void ZeroAndBelowIdTest(string inputId)
            {
                Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
                int id = int.Parse(inputId);

                playerRepository.Setup(x => x.FindAsync(id)).Throws<Exception>();
                PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()), _mapper);

                var result = await playerController.Get(inputId) as BadRequestObjectResult;

                Assert.NotNull(result);
                Assert.IsType<BadRequestObjectResult>(result);
            }

            [Theory]
            [InlineData("abdc")]
            [InlineData("something awesome")]
            [InlineData("a")]
            [InlineData("lets go lalalal")]
            public async void InvalidIdTypeTest(string inputId)
            {
                Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();

                playerRepository.Setup(x => x.FindAsync(0)).Throws<Exception>();
                PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()), _mapper);
                var result = await playerController.Get("invalid_id_value") as BadRequestObjectResult;

                Assert.NotNull(result);
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        public class GetPlayersTest
        {
            private readonly IMapper _mapper;

            public GetPlayersTest()
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Player, PlayerDto>().ReverseMap();
                    cfg.CreateMap<PlayerGames, PlayerGamesDto>().ReverseMap();
                    cfg.CreateMap<PlayerGroups, PlayerGroupsDto>().ReverseMap();
                    cfg.CreateMap<PlayerTeams, PlayerTeamsDto>().ReverseMap();
                });
                _mapper = config.CreateMapper();
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
