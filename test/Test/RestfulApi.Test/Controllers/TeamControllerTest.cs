using System;
using System.Collections.Generic;
using AutoMapper;
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
        private static readonly Mock<ILogger<TeamController>> Logger = new Mock<ILogger<TeamController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        private static readonly TeamController TeamController =
            new TeamController(TeamRepository.Object, Logger.Object, Mapper.Object);


        public static List<Mock> Mocks()
        {
            return new List<Mock>
            {
                TeamRepository,
                Logger,
                Mapper
            };
        }

        public class DeleteTeamTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNotFoundIfTeamDosentExist(int id)
            {
                MockExtensions.ResetAll(Mocks());

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await TeamController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNoContentResultIfATeamIsDeletedWithValidData(int id)
            {
                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                TeamRepository.Setup(x => x.Delete(id));
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await TeamController.Update(id, new TeamDto() {TeamId = id});
                Assert.IsType<NoContentResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetObjectResultIfDataIsNotDeleteWithValidTeamId(int id)
            {
                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                TeamRepository.Setup(x => x.Delete(id));
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await TeamController.Delete(id);
                Assert.IsType<ObjectResult>(result);
            }
        }

        public class UpdateTeamTest
        {
            [Fact]
            public async void GetBadRequestTypeIfTeamDtoIsNull()
            {
                MockExtensions.ResetAll(Mocks());

                TeamDto teamDto = null;

                var result = await TeamController.Update(teamDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNotFoundIfTeamDosentExist(int id)
            {
                MockExtensions.ResetAll(Mocks());

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await TeamController.Update(new TeamDto() {TeamId = id});

                Assert.IsType<NotFoundResult>(result);
            }


            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetObjectResultIfDataIsNotUpdateWithValidTeamDto(int id)
            {
                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                TeamRepository.Setup(x => x.Update(instance));
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await TeamController.Update(new TeamDto() {TeamId = id});
                Assert.IsType<ObjectResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNoContentResultIfATeamIsUpdateWithValidData(int id)
            {
                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                TeamRepository.Setup(x => x.Update(instance));
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await TeamController.Update(new TeamDto() {TeamId = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostTeamTest
        {
            [Fact]
            public async void GetCreateAtRouteResultIfDataIsValid()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);

                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                TeamRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Team>(It.IsAny<TeamDto>())).Returns(instance);

                var result = await TeamController.Create(new TeamDto());
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public async void GetObjectResultIfValiddDataIsNotSaved()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                TeamRepository.Setup(x => x.Insert(instance));

                var result = await TeamController.Create(new TeamDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void GetBadRequestTypeIfTeamDtoIsNull()
            {
                MockExtensions.ResetAll(Mocks());

                TeamDto teamDto = null;

                var result = await TeamController.Create(teamDto);

                Assert.IsType<BadRequestObjectResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void CheckIfCreateAtRouteIsCreatedWithRightValuesWhenAValidTeamIsCreated(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                instance.TeamId = id;
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                TeamRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Team>(It.IsAny<TeamDto>())).Returns(instance);

                var result = await TeamController.Create(new TeamDto()) as CreatedAtRouteResult;
                var routeValue = (int) result.RouteValues["Id"];
                Assert.Equal(routeValue, id);
            }

            [Fact]
            public async void CheckIfCreatedAtRouteObjectIsTeamWhenAValidTeamIsSaved()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                TeamRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Team>(It.IsAny<TeamDto>())).Returns(instance);

                var result = await TeamController.Create(new TeamDto()) as CreatedAtRouteResult;
                var routeObject = result.Value as TeamDto;
                Assert.IsType<TeamDto>(routeObject);
            }
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
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<TeamDto>(It.IsAny<Team>())).Returns(new TeamDto());

                var result = await TeamController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnJsonAsResultTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await TeamController.Get();
                Assert.IsType<JsonResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IsTypeOfTeamDtoTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                Mapper.Setup(m => m.Map<TeamDto>(It.IsAny<Team>())).Returns(new TeamDto());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                var result = await TeamController.Get(id) as JsonResult;
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
                MockExtensions.ResetAll(Mocks());
                var result = await TeamController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}