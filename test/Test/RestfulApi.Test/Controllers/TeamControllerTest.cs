using AutoMapper;
using Data.App.Models.Builders.TeamBuilders;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App.Controllers;
using RestfulApi.App.Dtos.TeamDtos;
using Xunit;

namespace Test.RestfulApi.Test.Controllers
{
    public class TeamControllerTest
    {
        private static readonly Mock<ITeamRepository> TeamRepository = new Mock<ITeamRepository>();
        private static readonly Mock<ITeamBuilder> TeamBuilder = new Mock<ITeamBuilder>();
        private static readonly Mock<ILogger<TeamController>> Logger = new Mock<ILogger<TeamController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
        private static TeamController _teamController;

        public class PostTeamTest
        {


        }
        public class GetTeamTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void ReturnJsonAsResultWithIdInputTest(int id)
            {
                _teamController = new TeamController(TeamRepository.Object,
                    Logger.Object, Mapper.Object);
                var result = await _teamController.Get(id);

                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnJsonAsResultTest()
            {
                _teamController = new TeamController(TeamRepository.Object,
                    Logger.Object, Mapper.Object);
                var result = await _teamController.Get();

                Assert.IsType<JsonResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IsTypeOfTeamDtoTest(int id)
            {
                Mapper.Setup(m => m.Map<TeamDto>(It.IsAny<Team>())).Returns(new TeamDto());
                _teamController = new TeamController(TeamRepository.Object,
                    Logger.Object, Mapper.Object);

                var result = await _teamController.Get(id) as JsonResult;
                var teamDto = result.Value as TeamDto;
                Assert.IsType<TeamDto>(teamDto);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-50)]
            [InlineData(-100000)]
            public async void InvalidInputTest(int id)
            {
                _teamController = new TeamController(TeamRepository.Object,
                    Logger.Object, Mapper.Object);

                var result = await _teamController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

    }
}