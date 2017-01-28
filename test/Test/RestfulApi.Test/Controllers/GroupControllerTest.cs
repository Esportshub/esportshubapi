using System;
using System.Collections.Generic;
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
            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNotFoundIfGroupDosentExist(int id)
            {
                MockExtensions.ResetAll(Mocks());

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await GroupController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNoContentResultIfAGroupIsDeletedWithValidData(int id)
            {
                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                GroupRepository.Setup(x => x.Delete(id));
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await GroupController.Update(id, new GroupDto() {GroupId = id});
                Assert.IsType<NoContentResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetObjectResultIfDataIsNotDeleteWithValidGroupId(int id)
            {
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
            public async void GetBadRequestTypeIfGroupDtoIsNull()
            {
                MockExtensions.ResetAll(Mocks());

                GroupDto groupDto = null;

                var result = await GroupController.Update(1, groupDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNotFoundIfGroupDosentExist(int id)
            {
                MockExtensions.ResetAll(Mocks());

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await GroupController.Update(id, new GroupDto() {GroupId = id});

                Assert.IsType<NotFoundResult>(result);
            }


            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetObjectResultIfDataIsNotUpdateWithValidGroupDto(int id)
            {
                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                GroupRepository.Setup(x => x.Update(instance));
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await GroupController.Update(id, new GroupDto() {GroupId = id});
                Assert.IsType<ObjectResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNoContentResultIfAGroupIsUpdateWithValidData(int id)
            {
                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                GroupRepository.Setup(x => x.Update(instance));
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await GroupController.Update(id, new GroupDto() {GroupId = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostGroupTest
        {
            [Fact]
            public async void GetCreateAtRouteResultIfDataIsValid()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);

                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                GroupRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Group>(It.IsAny<GroupDto>())).Returns(instance);

                var result = await GroupController.Create(new GroupDto());
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public async void GetObjectResultIfValiddDataIsNotSaved()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                GroupRepository.Setup(x => x.Insert(instance));

                var result = await GroupController.Create(new GroupDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void GetBadRequestTypeIfGroupDtoIsNull()
            {
                MockExtensions.ResetAll(Mocks());

                GroupDto groupDto = null;

                var result = await GroupController.Create(groupDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void CheckIfCreateAtRouteIsCreatedWithRightValuesWhenAValidGroupIsCreated(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                instance.GroupId = id;
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                GroupRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Group>(It.IsAny<GroupDto>())).Returns(instance);

                var result = await GroupController.Create(new GroupDto()) as CreatedAtRouteResult;
                var routeValue = (int) result.RouteValues["Id"];
                Assert.Equal(routeValue, id);
            }

            [Fact]
            public async void CheckIfCreatedAtRouteObjectIsGroupWhenAValidTeamIsSaved()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);
                GroupRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                GroupRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Group>(It.IsAny<GroupDto>())).Returns(instance);

                var result = await GroupController.Create(new GroupDto()) as CreatedAtRouteResult;
                var routeObject = result.Value as GroupDto;
                Assert.IsType<GroupDto>(routeObject);
            }
        }

        public class GetGroupTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void ReturnJsonAsResultWithIdInputTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Group) Activator.CreateInstance(typeof(Group), nonPublic: true);

                GroupRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<GroupDto>(It.IsAny<Group>())).Returns(new GroupDto());

                var result = await GroupController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnJsonAsResultTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await GroupController.Get();
                Assert.IsType<JsonResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IsTypeOfGroupeDtoTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                Mapper.Setup(m => m.Map<GroupDto>(It.IsAny<Group>())).Returns(new GroupDto());

                var result = await GroupController.Get(id) as JsonResult;
                var teamDto = result.Value as GroupDto;
                Assert.IsType<GroupDto>(teamDto);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-50)]
            [InlineData(-100000)]
            public async void InvalidInputTest(int id)
            {
                MockExtensions.ResetAll(Mocks());
                var result = await GroupController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}
