//using System;
//using System.Collections.Generic;
//using System.Diagnostics.Tracing;
//using AutoMapper;
//using Data.App.Models.Entities;
//using Data.App.Models.Entities.Events;
//using Data.App.Models.Repositories.Activities;
//using Data.App.Models.Repositories.Events;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using RestfulApi.App.Controllers;
//using RestfulApi.App.Dtos.ActivitiesDtos;
//using RestfulApi.App.Dtos.EventsDtos;
//using Xunit;
//
//namespace Test.RestfulApi.Test.Controllers
//{
//    public class GameEventControllerTest
//    {
//        private static readonly Mock<IEventRepository> EventRepository = new Mock<IEventRepository>();
//        private static readonly Mock<ILogger<EventController>> Logger = new Mock<ILogger<EventController>>();
//        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();
//
//        private static readonly EventController EventController =
//            new EventController(EventRepository.Object, Logger.Object, Mapper.Object);
//
//
//        public static List<Mock> Mocks()
//        {
//            return new List<Mock>
//            {
//                EventRepository,
//                Logger,
//                Mapper
//            };
//        }
//
//
//        public class DeleteGameEventTest
//        {
//            [Theory]
//            [InlineData(1)]
//            [InlineData(22)]
//            [InlineData(5032)]
//            [InlineData(100000)]
//            public async void GetNotFoundIfGameEventDosentExist(int id)
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
//
//                var result = await EventController.Delete(id);
//
//                Assert.IsType<NotFoundResult>(result);
//            }
//
//            [Theory]
//            [InlineData(1)]
//            [InlineData(22)]
//            [InlineData(5032)]
//            [InlineData(100000)]
//            public async void GetNoContentResultIfAGameEventIsDeletedWithValidData(int id)
//            {
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
//                EventRepository.Setup(x => x.Delete(id));
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
//
//                var result = await EventController.Update(new GameEventDto() {EventId = id});
//                Assert.IsType<NoContentResult>(result);
//            }
//
//            [Theory]
//            [InlineData(1)]
//            [InlineData(22)]
//            [InlineData(5032)]
//            [InlineData(100000)]
//            public async void GetObjectResultIfDataIsNotDeleteWithValidGameEventId(int id)
//            {
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
//                EventRepository.Setup(x => x.Delete(id));
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
//
//                var result = await EventController.Delete(id);
//                Assert.IsType<ObjectResult>(result);
//            }
//        }
//
//        public class UpdateGameEventTest
//        {
//            [Fact]
//            public async void GetBadRequestTypeIfGameEventDtoIsNull()
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                GameEventDto gameEventDto = null;
//
//                var result = await EventController.Update(gameEventDto);
//
//                Assert.IsType<BadRequestResult>(result);
//            }
//
//            [Theory]
//            [InlineData(1)]
//            [InlineData(22)]
//            [InlineData(5032)]
//            [InlineData(100000)]
//            public async void GetNotFoundIfGameEventDosentExist(int id)
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
//                var result = await EventController.Update(new GameEventDto() {EventId = id});
//
//                Assert.IsType<NotFoundResult>(result);
//            }
//
//
//            [Theory]
//            [InlineData(1)]
//            [InlineData(22)]
//            [InlineData(5032)]
//            [InlineData(100000)]
//            public async void GetObjectResultIfGameEventIsNotUpdateWithValidEventDto(int id)
//            {
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
//                EventRepository.Setup(x => x.Update(instance));
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
//
//                var result = await EventController.Update(new GameEventDto() {EventId = id});
//                Assert.IsType<ObjectResult>(result);
//            }
//
//            [Theory]
//            [InlineData(1)]
//            [InlineData(22)]
//            [InlineData(5032)]
//            [InlineData(100000)]
//            public async void GetNoContentResultIfAGameEventIsUpdateWithValidData(int id)
//            {
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
//                EventRepository.Setup(x => x.Update(instance));
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
//
//                var result = await EventController.Update(new GameEventDto() {EventId = id});
//                Assert.IsType<NoContentResult>(result);
//            }
//        }
//
//        public class PostGameEventTest
//        {
//            [Fact]
//            public async void GetCreateAtRouteResultIfDataIsValid()
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
//                EventRepository.Setup(x => x.Insert(instance));
//                Mapper.Setup(m => m.Map<GameEvent>(It.IsAny<GameEventDto>())).Returns(instance);
//
//                var result = await EventController.Create(new GameEventDto());
//                Assert.IsType<CreatedAtRouteResult>(result);
//            }
//
//            [Fact]
//            public async void GetObjectResultIfValiddDataIsNotSaved()
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
//                EventRepository.Setup(x => x.Insert(instance));
//
//                var result = await EventController.Create(new GameEventDto());
//                Assert.IsType<ObjectResult>(result);
//            }
//
//            [Fact]
//            public async void GetBadRequestTypeIfGameEventDtoIsNull()
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                GameEventDto gameEventDto = null;
//
//                var result = await EventController.Create(gameEventDto);
//
//                Assert.IsType<BadRequestResult>(result);
//            }
//
//            [Theory]
//            [InlineData(1)]
//            [InlineData(37)]
//            [InlineData(50000)]
//            [InlineData(100000)]
//            public async void CheckIfCreateAtRouteIsCreatedWithRightValuesWhenAValidGameEventIsCreated(int id)
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//                instance.EventId = id;
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
//                EventRepository.Setup(x => x.Insert(instance));
//                Mapper.Setup(m => m.Map<GameEvent>(It.IsAny<GameEventDto>())).Returns(instance);
//
//                var result = await EventController.Create(new GameEventDto()) as CreatedAtRouteResult;
//                var routeValue = (int) result.RouteValues["Id"];
//                Assert.Equal(routeValue, id);
//            }
//
//            [Fact]
//            public async void CheckIfCreatedAtRouteObjectIsEventWhenAValidGameEventIsSaved()
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
//                EventRepository.Setup(x => x.Insert(instance));
//                Mapper.Setup(m => m.Map<GameEvent>(It.IsAny<GameEventDto>())).Returns(instance);
//
//                var result = await EventController.Create(new GameEventDto()) as CreatedAtRouteResult;
//                var routeObject = result.Value as GameEventDto;
//                Assert.IsType<GameEventDto>(routeObject);
//            }
//        }
//
//        public class GetGameEventTest
//        {
//            [Theory]
//            [InlineData(1)]
//            [InlineData(37)]
//            [InlineData(50000)]
//            [InlineData(100000)]
//            public async void ReturnJsonAsResultWithIdInputTest(int id)
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                var instance = (GameEvent) Activator.CreateInstance(typeof(GameEvent), nonPublic: true);
//
//                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
//                Mapper.Setup(m => m.Map<GameEventDto>(It.IsAny<GameEvent>())).Returns(new GameEventDto());
//
//                var result = await EventController.Get(id) as JsonResult;
//                Assert.IsType<JsonResult>(result);
//            }
//
//            [Fact]
//            public async void ReturnJsonAsResultTest()
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                var result = await EventController.Get();
//                Assert.IsType<JsonResult>(result);
//            }
//
//            [Theory]
//            [InlineData(1)]
//            [InlineData(37)]
//            [InlineData(50000)]
//            [InlineData(100000)]
//            public async void IsTypeOfEventDtoTest(int id)
//            {
//                MockExtensions.ResetAll(Mocks());
//
//                Mapper.Setup(m => m.Map<GameEventDto>(It.IsAny<GameEvent>())).Returns(new GameEventDto());
//
//                var result = await EventController.Get(id) as JsonResult;
//                var teamDto = result.Value as EventDto;
//                Assert.IsType<EventDto>(teamDto);
//            }
//
//            [Theory]
//            [InlineData(0)]
//            [InlineData(-1)]
//            [InlineData(-50)]
//            [InlineData(-100000)]
//            public async void InvalidInputTest(int id)
//            {
//                MockExtensions.ResetAll(Mocks());
//                var result = await EventController.Get(id) as BadRequestObjectResult;
//
//                Assert.IsType<BadRequestObjectResult>(result);
//            }
//        }
//    }
//}