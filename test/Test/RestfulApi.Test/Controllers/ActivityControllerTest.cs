using System;
using System.Collections.Generic;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Activities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App.Controllers;
using RestfulApi.App.Dtos.ActivitiesDtos;
using Xunit;

namespace Test.RestfulApi.Test.Controllers
{
    public class ActivityControllerTest
    {
        private static readonly Mock<IActivityRepository> ActivityRepository = new Mock<IActivityRepository>();
        private static readonly Mock<ILogger<ActivityController>> Logger = new Mock<ILogger<ActivityController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        private static readonly ActivityController ActivityController =
            new ActivityController(ActivityRepository.Object, Logger.Object, Mapper.Object);


        public static List<Mock> Mocks()
        {
            return new List<Mock>
            {
                ActivityRepository,
                Logger,
                Mapper
            };
        }

        public class DeleteActivityTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNotFoundResultIfActivityDoesntExistTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await ActivityController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNoContentResultIfActivityIsDeletedWithValidDataTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                ActivityRepository.Setup(x => x.Delete(id));
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                Mapper.Setup(x => x.Map<Activity>(It.IsAny<ActivityDto>())).Returns(instance);


                var result = await ActivityController.Delete(id);
                Assert.IsType<NoContentResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsObjectResultWithStatusCode500IfDataIsNotDeleteWithValidActivityIdTest(int id)
            {
                var instance = (Activity)Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                ActivityRepository.Setup(x => x.Delete(id));
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await ActivityController.Delete(id);
                Assert.IsType<ObjectResult>(result);
            }
        }

        public class UpdateActivityTest
        {
            [Fact]
            public async void ReturnsBadRequestResultIfActivityDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                ActivityDto activityDto = null;

                var result = await ActivityController.Update(1, activityDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNotFoundResultIfActivityDoesntExistTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await ActivityController.Update(id, new ActivityDto() {ActivityId = id});

                Assert.IsType<NotFoundResult>(result);
            }


            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsObjectResultWithStatusCode500IfDataIsNotUpdatedWithValidActivityDtoTest(int id)
            {
                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                ActivityRepository.Setup(x => x.Update(instance));
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await ActivityController.Update(id, new ActivityDto() {ActivityId = id});
                Assert.IsType<ObjectResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNoContentResultIfActivityIsUpdatedWithValidDataTest(int id)
            {
                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                ActivityRepository.Setup(x => x.Update(instance));
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await ActivityController.Update(id, new ActivityDto() {ActivityId = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostActivityTest
        {
            [Fact]
            public async void ReturnsCreateAtRouteResultIfDataIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);

                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                ActivityRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Activity>(It.IsAny<ActivityDto>())).Returns(instance);

                var result = await ActivityController.Create(new ActivityDto());
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfValidDataIsNotSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                ActivityRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Activity>(It.IsAny<ActivityDto>())).Returns(instance);

                var result = await ActivityController.Create(new ActivityDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsBadRequestTypeIfActivityDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                ActivityDto activityDto = null;

                var result = await ActivityController.Create(activityDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IfCreatedAtRouteResultIsCreatedWithRightValuesWhenAValidActivityIsCreatedTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                instance.ActivityId = id;
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                ActivityRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Activity>(It.IsAny<ActivityDto>())).Returns(instance);

                var result = await ActivityController.Create(new ActivityDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                var routeValue = (int) result.RouteValues["Id"];
                Assert.Equal(routeValue, id);
            }

            [Fact]
            public async void IfCreatedAtRouteResultIsActivityWhenAValidActivityIsSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                ActivityRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Activity>(It.IsAny<ActivityDto>())).Returns(instance);

                var result = await ActivityController.Create(new ActivityDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                var routeObject = result.Value as ActivityDto;
                Assert.IsType<ActivityDto>(routeObject);
            }
        }

        public class GetActivityTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void ReturnsJsonAsResultWhenValidIdTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);

                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<ActivityDto>(It.IsAny<Activity>())).Returns(new ActivityDto());

                var result = await ActivityController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IsTypeOfActivityDtoTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                Mapper.Setup(m => m.Map<ActivityDto>(It.IsAny<Activity>())).Returns(new ActivityDto());

                var result = await ActivityController.Get(id) as JsonResult;
                Assert.NotNull(result);
                var activityDto = result.Value as ActivityDto;
                Assert.NotNull(activityDto);
                Assert.IsType<ActivityDto>(activityDto);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-50)]
            [InlineData(-100000)]
            public async void ReturnsBadRequestObjectResultWhenInvalidIdTest(int id)
            {
                MockExtensions.ResetAll(Mocks());
                var result = await ActivityController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        public class GetActivitiesTest
        {
            [Fact]
            public async void ReturnsJsonAsResultTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await ActivityController.Get();
                Assert.IsType<JsonResult>(result);
            }

        }
    }
}