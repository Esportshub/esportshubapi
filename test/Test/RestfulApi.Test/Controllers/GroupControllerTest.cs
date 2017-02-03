using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App.Controllers;
using RestfulApi.App.Dtos.GroupDtos;
using Xunit;

namespace Test.RestfulApi.Test.Controllers
{
    public class GroupontrollerTest
    {
        private static readonly Mock<IGroupRepository> GroupRepository = new Mock<IGroupRepository>();
        private static readonly Mock<ILogger<GroupController>> Logger = new Mock<ILogger<GroupController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        private static readonly GroupController GroupController =
            new GroupController(GroupRepository.Object, Logger.Object, Mapper.Object);


        public static List<Mock> Mocks()
        {
            return new List<Mock>
            {
                GroupRepository,
                Logger,
                Mapper
            };
        }

        public class DeleteGroupTest
        {
            [Fact]
            public async void ReturnsNotFoundResultIfGroupDoesntExistWithValidIdtTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await GroupController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfAGroupIsDeletedWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                var id = Guid.NewGuid();
                instance.GroupGuid = id;

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                GroupRepository.Setup(x => x.Delete(id));
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await GroupController.Delete(id);

                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfDataIsNotDeleteWithValidGroupIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Group)Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                GroupRepository.Setup(x => x.Delete(id));
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await GroupController.Delete(id);
                Assert.IsType<ObjectResult>(result);
            }
        }
        public class UpdateGroupTest
        {
            [Fact]
            public async void ReturnBadRequestResultTypeIfGroupDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await GroupController.Update(new Guid(), null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsNotFoundResultIfGroupDoesntExistWithValidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await GroupController.Update(id, new GroupDto() { GroupGuid = id});

                Assert.IsType<NotFoundResult>(result);
            }


            [Fact]
            public async void ReturnsObjectResultIfDataIsNotUpdateWithValidGroupDtoTest()
            {

                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                GroupRepository.Setup(x => x.Update(instance));
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await GroupController.Update(id, new GroupDto() {GroupGuid = id});
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfAGroupIsUpdateWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                GroupRepository.Setup(x => x.Update(instance));
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await GroupController.Update(id, new GroupDto() {GroupGuid = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostGroupTest
        {
            [Fact]
            public async void ReturnsCreatedAtRouteResultIfGroupDtoIsValid()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                instance.GroupGuid = id;

                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                GroupRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Group>(It.IsAny<GroupDto>())).Returns(instance);

                var result = await GroupController.Create(new GroupDto());
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfValidGroupIsNotSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                GroupRepository.Setup(x => x.Insert(instance));

                var result = await GroupController.Create(new GroupDto()) as ObjectResult;
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultWithStatusCode500IfValidGroupIsNotSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                GroupRepository.Setup(x => x.Insert(instance));

                var result = await GroupController.Create(new GroupDto()) as ObjectResult;
                Assert.IsType<ObjectResult>(result);
                Assert.Equal(500, result.StatusCode);
            }

            [Fact]
            public async void ReturnsBadRequestTypeIfGroupDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await GroupController.Create(null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void IfCreatedAtRouteIsCreatedWithRightValuesWhenAValidGroupIsCreatedTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                instance.GroupGuid = id;
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                GroupRepository.Setup(x => x.Insert(instance));
                var groupDto = new GroupDto {GroupGuid = id};
                Mapper.Setup(m => m.Map<Group>(groupDto)).Returns(instance);

                var result = await GroupController.Create(groupDto) as CreatedAtRouteResult;
                Assert.NotNull(result);
                Guid guid;
                Assert.True(Guid.TryParse((String)result.RouteValues["id"], out guid));
                Assert.True(id == guid);
            }

            [Fact]
            public async void IfCreatedAtRouteObjectIsGroupDtoWhenAValidGroupIsSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                GroupRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Group>(It.IsAny<GroupDto>())).Returns(instance);

                var result = await GroupController.Create(new GroupDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                var routeObject = result.Value as GroupDto;
                Assert.IsType<GroupDto>(routeObject);
            }
        }

        public class GetGroupTest
        {
            [Fact]
            public async void ReturnsJsonResultWhenIdIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                instance.GroupGuid = id;

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                var groupDto = new GroupDto {GroupGuid = id};
                Mapper.Setup(m => m.Map<GroupDto>(It.IsAny<Group>())).Returns(groupDto);

                var result = await GroupController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnsJsonResultWithValueGroupDtoWhenIdIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                Mapper.Setup(m => m.Map<GroupDto>(It.IsAny<Group>())).Returns(new GroupDto());

                var result = await GroupController.Get(id) as JsonResult;
                Assert.NotNull(result);
                var teamDto = result.Value as GroupDto;
                Assert.IsType<GroupDto>(teamDto);
            }

            [Fact]
            public async void ReturnsBadRequestObjectResultWhenInvalidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.Empty;
                var result = await GroupController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        public class GetGroupsTest
        {
            private IEnumerable<Group> GetGroups(Guid[] groupIds)
            {
                IEnumerable<Group> groups = new List<Group>();
                foreach (var groupId in groupIds)
                {
                    var group = (Group) Activator.CreateInstance(typeof(Group), true);
                    group.GroupGuid = groupId;
                    groups.Append(group);
                }
                return groups;
            }

            [Fact]
            public async void ReturnsJsonResultWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var groupIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
                var groups = GetGroups(groupIds);

                GroupRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Group, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(groups);
                foreach (var groupId in groupIds)
                {
                    var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                    instance.GroupGuid = groupId;
                    var groupDto = new GroupDto {GroupGuid = groupId};
                    Mapper.Setup(x => x.Map<GroupDto>(instance)).Returns(groupDto);
                }

                var esportshubEventDtos = await GroupController.Get() as JsonResult;
                Assert.IsType<JsonResult>(esportshubEventDtos);
            }

            [Fact]
            public async void ReturnsJsonResultWithIEnumerableGroupDtoAsValueWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var groupIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
                var groupEvents = GetGroups(groupIds);

                GroupRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Group, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(groupEvents);
                foreach (var groupId in groupIds)
                {
                    var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                    instance.GroupGuid = groupId;
                    var groupDto = new GroupDto {GroupGuid = groupId};
                    Mapper.Setup(x => x.Map<GroupDto>(instance)).Returns(groupDto);
                }

                var result = await GroupController.Get() as JsonResult;
                Assert.NotNull(result);
                var groupDtos = result.Value as IEnumerable<GroupDto>;
                Assert.NotNull(groupDtos);
                foreach (var groupDto in groupDtos)
                {
                    Assert.True(groupIds.Contains(groupDto.GroupGuid));
                }
            }

            [Fact]
            public async void ReturnsNotFoundResultWhenItDoesntFindAnythingTest()
            {
                MockExtensions.ResetAll(Mocks());
                GroupRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Group, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(null);

                var result = await GroupController.Get() as NotFoundResult;
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
