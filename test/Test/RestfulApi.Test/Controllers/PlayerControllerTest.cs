using System;
using System.Threading.Tasks;
using AutoMapper;
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

        private static readonly Mock<IPlayerRepository> PlayerRepository = new Mock<IPlayerRepository>();
        private static readonly Mock<ILogger<PlayerController>> Logger = new Mock<ILogger<PlayerController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        public class GetPlayerTest
        {

            public GetPlayerTest()
            {
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void ReturnCorrectType(int id)
            {
                Player player = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                player.PlayerId = id;
                PlayerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(player));
                PlayerController playerController = new PlayerController(PlayerRepository.Object, new Logger<PlayerController>(new LoggerFactory()), _mapper);
                var jsonResult = await playerController.Get(id);

                Assert.IsType<JsonResult>(jsonResult);
            }

            public PlayerDto CreatePlayerDto(int id, string nickName)
            {
                PlayerDto playerDto = new PlayerDto();
                playerDto.PlayerId = id;
                playerDto.Nickname = nickName;
                return playerDto;
            }

            [Theory]
            [InlineData(1, "Sjuften")]
            [InlineData(37, "DenLilleMand")]
            [InlineData(50000, "Killer")]
            [InlineData(100000, "")]
            public async void CorrectIdTest(int id, string nickName)
            {
                Player player = (Player) Activator.CreateInstance(typeof(Player), nonPublic: true);
                PlayerDto playerDto = CreatePlayerDto(id, nickName);

                Mapper.Setup(mapper => mapper.Map<PlayerDto>(It.IsAny<Player>())).Returns(playerDto);
                PlayerRepository.Setup(x => x.FindAsync(id)).Returns(Task.FromResult(player));
                PlayerController playerController = new PlayerController(PlayerRepository.Object, Logger.Object, Mapper.Object);
                JsonResult jsonResult = await playerController.Get(id) as JsonResult;
                Assert.NotNull(jsonResult);
                PlayerDto playerDtoResult = jsonResult.Value as PlayerDto;

                Assert.NotNull(playerDtoResult);
                Assert.Equal(id, playerDtoResult.PlayerId);
                Assert.Equal("DenLilleMand", playerDtoResult.Nickname);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-50)]
            [InlineData(-100000)]
            public async void ZeroAndBelowIdTest(int id)
            {
                Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();

                playerRepository.Setup(x => x.FindAsync(id)).Throws<Exception>();
                PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()), _mapper);

                var result = await playerController.Get(id) as BadRequestObjectResult;

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

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-50)]
            [InlineData(-100000)]
            public async void ZeroAndBelowIdTest(int id)
            {
                Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();

                playerRepository.Setup(x => x.FindAsync(id)).Throws<Exception>();
                PlayerController playerController = new PlayerController(playerRepository.Object, new Logger<PlayerController>(new LoggerFactory()), _mapper);

                var result = await playerController.Get(id) as BadRequestObjectResult;

                Assert.NotNull(result);
                Assert.IsType<BadRequestObjectResult>(result);
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
