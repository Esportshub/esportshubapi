using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            [Fact]
            public async void ReturnsNotFoundIfTeamDosentExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.Empty;

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await TeamController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultTypeIfATeamIsDeletedWithValidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                instance.TeamGuid = id;
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                TeamRepository.Setup(x => x.Delete(id));
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await TeamController.Update(id, new TeamDto() {TeamGuid = id});
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfDataIsNotDeleteWithValidTeamIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                instance.TeamGuid = id;

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
            public async void ReturnsBadRequestResultTypeIfTeamDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await TeamController.Update(new Guid(), null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsNotFoundIfTeamDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await TeamController.Update(id, new TeamDto() { TeamGuid = id });

                Assert.IsType<NotFoundResult>(result);
            }


            [Fact]
            public async void ReturnsObjectResultIfTeamIsNotUpdatedWithValidTeamDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                TeamRepository.Setup(x => x.Update(instance));
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await TeamController.Update(id, new TeamDto() {TeamGuid = id});

                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void GetNoContentResultIfATeamIsUpdatedWithValidTeamDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                TeamRepository.Setup(x => x.Update(instance));
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await TeamController.Update(id, new TeamDto() {TeamGuid = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostTeamTest
        {
            [Fact]
            public async void ReturnsCreatedAtRouteResultIfTeamDtoIsValidTest()
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
            public async void ReturnsObjectResultIfValidTeamIsNotSavedWithValidTeamDtoTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                TeamRepository.Setup(x => x.Insert(instance));

                var result = await TeamController.Create(new TeamDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultWithStatusCode500IfValidTeamIsNotSavedWithValidTeamDtoTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                TeamRepository.Setup(x => x.Insert(instance));

                var result = await TeamController.Create(new TeamDto()) as ObjectResult;
                Assert.NotNull(result);
                Assert.NotNull(result.StatusCode);
                Assert.Equal(500, result.StatusCode.Value);
            }

            [Fact]
            public async void ReturnsBadRequestResultTypeIfTeamDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await TeamController.Create(null);

                Assert.IsType<BadRequestObjectResult>(result);
            }

            [Fact]
            public async void ReturnsCreatedAtRouteResultTypeWhenValidTeamIsSavedWithValidTeamDtoTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                TeamRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Team>(It.IsAny<TeamDto>())).Returns(instance);

                var result = await TeamController.Create(new TeamDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                var routeObject = result.Value as TeamDto;
                Assert.IsType<TeamDto>(routeObject);
            }

            [Fact]
            public async void ReturnsCorrectCreatedAtRouteResultWhenAValidTeamIsCreatedWithValidTeamDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                instance.TeamGuid = id;

                TeamRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                TeamRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Team>(It.IsAny<TeamDto>())).Returns(instance);

                var result = await TeamController.Create(new TeamDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                Guid guid;
                Assert.True(Guid.TryParse((string) result.RouteValues["Id"], out guid));
                Assert.True(id == guid);
            }

        }

        public class GetTeamTest
        {
            [Fact]
            public async void ReturnsJsonResultWithValidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                instance.TeamGuid = id;

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<TeamDto>(It.IsAny<Team>())).Returns(new TeamDto());

                var result = await TeamController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnsJsonResultWithValueOfTypeTeamDtoWhenIdIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();
                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                instance.TeamGuid = id;

                var teamDto = new TeamDto {TeamGuid = id};

                Mapper.Setup(m => m.Map<TeamDto>(instance)).Returns(teamDto);

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                var result = await TeamController.Get(id) as JsonResult;
                Assert.NotNull(result);
                var value = result.Value as TeamDto;
                Assert.IsType<TeamDto>(value);
            }

            [Fact]
            public async void ReturnsJsonResultWithCorrectTeamDtoValuesWhenIdIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                instance.TeamGuid = id;
                var teamDto = new TeamDto {TeamGuid = id};

                TeamRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<TeamDto>(instance)).Returns(teamDto);

                var result = await TeamController.Get(id) as JsonResult;
                Assert.NotNull(result);
                var resultTeamDto = result.Value as TeamDto;
                Assert.NotNull(resultTeamDto);
                Assert.True(id == resultTeamDto.TeamGuid);
            }

            [Fact]
            public async void ReturnsBadRequestObjectResultWhenIdIsInvalidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var result = await TeamController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        public class GetTeamsTest
        {
            private IEnumerable<Team> GetTeams(Guid[] teamIds)
            {
                IEnumerable<Team> teams = new List<Team>();
                foreach (var teamId in teamIds)
                {
                    var team = (Team) Activator.CreateInstance(typeof(Team), true);
                    team.TeamGuid = teamId;
                    teams.Append(team);
                }
                return teams;
            }

            [Fact]
            public async void ReturnsJsonResultWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var teamIds = new[] {new Guid(), new Guid(), new Guid(), new Guid()};
                var teams = GetTeams(teamIds);

                TeamRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(teams);
                foreach (var teamId in teamIds)
                {
                    var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                    instance.TeamGuid = teamId;
                    var teamDto = new TeamDto {TeamGuid = teamId};
                    Mapper.Setup(x => x.Map<TeamDto>(instance)).Returns(teamDto);
                }

                var teamDtos = await TeamController.Get() as JsonResult;
                Assert.IsType<JsonResult>(teamDtos);
            }

            [Fact]
            public async void ReturnsJsonResultWithIEnumerableTeamDtoAsValueWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var teamIds = new[] {new Guid(), new Guid(), new Guid(), new Guid()};
                var teams = GetTeams(teamIds);

                TeamRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(teams);
                foreach (var teamId in teamIds)
                {
                    var instance = (Team) Activator.CreateInstance(typeof(Team), nonPublic: true);
                    instance.TeamGuid = teamId;
                    var teamDto = new TeamDto {TeamGuid = teamId};
                    Mapper.Setup(x => x.Map<TeamDto>(instance)).Returns(teamDto);
                }

                var result = await TeamController.Get() as JsonResult;
                Assert.NotNull(result);
                var teamDtos = result.Value as IEnumerable<TeamDto>;

                Assert.NotNull(teamDtos);

                foreach (var teamDto in teamDtos)
                {
                    Assert.True(teamIds.Contains(teamDto.TeamGuid));
                }
            }

            [Fact]
            public async void ReturnsNotFoundResultWhenItDoesntFindAnythingTest()
            {
                MockExtensions.ResetAll(Mocks());
                TeamRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(null);

                var result = await TeamController.Get() as NotFoundResult;
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}