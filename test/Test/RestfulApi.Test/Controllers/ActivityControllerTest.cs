using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            [Fact]
            public async void ReturnsNotFoundResultIfActivityDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var guid = Guid.NewGuid();

                ActivityRepository.Setup(x => x.FindAsync(guid)).ReturnsAsync(null);

                var result = await ActivityController.Delete(guid);

                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfActivityIsDeletedWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                ActivityRepository.Setup(x => x.Delete(id));
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                Mapper.Setup(x => x.Map<Activity>(It.IsAny<ActivityDto>())).Returns(instance);


                var result = await ActivityController.Delete(id);
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultWithStatusCode500IfDataIsNotDeleteWithValidActivityIdTest()
            {
                MockExtensions.ResetAll(Mocks());

                var id = Guid.NewGuid();
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
                var id = Guid.NewGuid();

                var result = await ActivityController.Update(id, null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsNotFoundResultIfActivityDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await ActivityController.Update(id, new ActivityDto() {ActivityGuid = id});

                Assert.IsType<NotFoundResult>(result);
            }


            [Fact]
            public async void ReturnsObjectResultWithStatusCode500IfDataIsNotUpdatedWithValidActivityDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                ActivityRepository.Setup(x => x.Update(instance));
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await ActivityController.Update(id, new ActivityDto() {ActivityGuid = id});
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfActivityIsUpdatedWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                ActivityRepository.Setup(x => x.Update(instance));
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await ActivityController.Update(id, new ActivityDto() {ActivityGuid = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class CreateActivityTest
        {
            [Fact]
            public async void ReturnsCreateAtRouteResultIfDataIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                instance.ActivityGuid = id;
                var activityDto = new ActivityDto {ActivityGuid = id};

                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                ActivityRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Activity>(activityDto)).Returns(instance);

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

                var result = await ActivityController.Create(null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void IfCreatedAtRouteResultIsCreatedWithCorrectValuesWhenAValidActivityIsCreatedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var id = Guid.NewGuid();
                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                instance.ActivityGuid = id;
                var activityDto = new ActivityDto() { ActivityGuid = id};
                ActivityRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                ActivityRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Activity>(It.IsAny<ActivityDto>())).Returns(instance);

                var result = await ActivityController.Create(activityDto) as CreatedAtRouteResult;
                Assert.NotNull(result);
                Guid guid;
                Assert.True(Guid.TryParse((String) result.RouteValues["Id"], out guid));
                Assert.Equal(id, guid);
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
            [Fact]
            public async void ReturnsJsonAsResultWhenValidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var guid = Guid.NewGuid();

                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                instance.ActivityGuid = guid;

                ActivityRepository.Setup(x => x.FindAsync(guid)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<ActivityDto>(It.IsAny<Activity>())).Returns(new ActivityDto());

                var result = await ActivityController.Get(guid) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void IsTypeOfActivityDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                instance.ActivityGuid = id;
                var activityDto = new ActivityDto {ActivityGuid = id};

                Mapper.Setup(m => m.Map<ActivityDto>(It.IsAny<Activity>())).Returns(activityDto);
                ActivityRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);

                var result = await ActivityController.Get(id) as JsonResult;
                Assert.NotNull(result);
                var value = result.Value as ActivityDto;
                Assert.NotNull(value);
                Assert.IsType<ActivityDto>(value);
            }

            [Fact]
            public async void ReturnsBadRequestObjectResultWhenInvalidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var guid = Guid.Empty;
                var result = await ActivityController.Get(guid) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        public class GetActivitiesTest
        {
            private IEnumerable<Activity> GetActivities(Guid[] activitiesIds)
            {
                IEnumerable<Activity> activities = new List<Activity>();
                foreach (var activityGuid in activitiesIds)
                {
                    var activity = (Activity) Activator.CreateInstance(typeof(Activity), true);
                    activity.ActivityGuid = activityGuid;
                    activities.Append(activity);
                }
                return activities;
            }

            [Fact]
            public async void ReturnsJsonResultWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var activitiesIds = new [] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
                var activities = GetActivities(activitiesIds: activitiesIds);

                ActivityRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Activity, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(activities);
                foreach (var activityId in activitiesIds)
                {
                    var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                    instance.ActivityGuid = activityId;
                    var activityDto = new ActivityDto {ActivityGuid = activityId};
                    Mapper.Setup(x => x.Map<ActivityDto>(instance)).Returns(activityDto);
                }

                var activityDtos = await ActivityController.Get() as JsonResult;
                Assert.IsType<JsonResult>(activityDtos);
            }

            [Fact]
            public async void ReturnsJsonResultWithIEnumerableActivityDtoAsValueWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var activitiesIds = new [] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
                var activities = GetActivities(activitiesIds: activitiesIds);

                ActivityRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Activity, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(activities);
                foreach (var activityId in activitiesIds)
                {
                    var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                    instance.ActivityGuid = activityId;
                    var activityDto = new ActivityDto {ActivityGuid = activityId};
                    Mapper.Setup(x => x.Map<ActivityDto>(instance)).Returns(activityDto);
                }

                var result = await ActivityController.Get() as JsonResult;
                Assert.NotNull(result);
                var activityDtos = result.Value as IEnumerable<ActivityDto>;

                Assert.NotNull(activityDtos);

                foreach (var activityDto in activityDtos)
                {
                    Assert.True(activitiesIds.Contains(activityDto.ActivityGuid));
                }
            }

            [Fact]
            public async void ReturnsNotFoundResultWhenItDoesntFindAnythingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var activitiesIds = new [] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
                ActivityRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Activity, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(null);
                foreach (var activityId in activitiesIds)
                {
                    var instance = (Activity) Activator.CreateInstance(typeof(Activity), nonPublic: true);
                    instance.ActivityGuid = activityId;
                    var activityDto = new ActivityDto {ActivityGuid = activityId};
                    Mapper.Setup(x => x.Map<ActivityDto>(instance)).Returns(activityDto);
                }

                var result = await ActivityController.Get() as NotFoundResult;
                Assert.IsType<NotFoundResult>(result);
            }
        }

    }
}
