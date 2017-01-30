using System;
using System.Collections.Generic;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.EsportshubEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App.Controllers;
using RestfulApi.App.Dtos.EsportshubEventsDtos;
using Xunit;

namespace Test.RestfulApi.Test.Controllers
{
    public class EsportshubEventControllerTest
    {
        private static readonly Mock<IEsportshubEventRepository> EventRepository = new Mock<IEsportshubEventRepository>();
        private static readonly Mock<ILogger<EsportshubEventController>> Logger = new Mock<ILogger<EsportshubEventController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        private static readonly EsportshubEventController EsportshubEventController =
            new EsportshubEventController(EventRepository.Object, Logger.Object, Mapper.Object);


        public static List<Mock> Mocks()
        {
            return new List<Mock>
            {
                EventRepository,
                Logger,
                Mapper
            };
        }


        public class DeleteEsportshubEventTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNotFoundIfEsportshubEventDoesntExistTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                EventRepository.Setup(x => x.Delete(id));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNoContentResultIfAEsportshubEventIsDeletedWithValidDataTest(int id)
            {
                var instance = (EsportshubEvent) Activator.CreateInstance(type: typeof(EsportshubEvent), nonPublic: true);
                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EventRepository.Setup(x => x.Delete(id));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await EsportshubEventController.Delete(id);
                Assert.IsType<NoContentResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsObjectResultIfDataIsNotDeletedWithValidEsportshubEventIdTest(int id)
            {
                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EventRepository.Setup(x => x.Delete(id));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Delete(id);
                Assert.IsType<ObjectResult>(result);
            }
        }

        public class UpdateEsportshubEventTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsBadRequestResultTypeIfEsportshubEventDtoIsNullTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventId = id;
                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                EventRepository.Setup(x => x.Update(instance));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                Mapper.Setup(x => x.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(null);

                EsportshubEventDto esportshubEventDto = null;

                var result = await EsportshubEventController.Update(1, esportshubEventDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNotFoundIfEsportshubEventDoesntExistTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                EventRepository.Setup(x => x.Update(instance));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Update(id, new EsportshubEventDto() { EsportshubEventId = id });

                Assert.IsType<NotFoundResult>(result);
            }


            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsObjectResultIfEsportshubEventIsNotUpdatedWithValidEventDtoTest(int id)
            {
                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventId = id;
                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EventRepository.Setup(x => x.Update(instance));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Update(id, new EsportshubEventDto() { EsportshubEventId = id });
                Assert.IsType<ObjectResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void ReturnsNoContentResultIfAEsportshubEventIsUpdatedWithValidDataTest(int id)
            {
                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventId = id;
                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EventRepository.Setup(x => x.Update(instance));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await EsportshubEventController.Update(id, new EsportshubEventDto() { EsportshubEventId = id });
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostEsportshubEventTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(35)]
            [InlineData(30000)]
            [InlineData(100000)]
            public async void ReturnsCreateAtRouteResultIfDataIsValidTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventId = id;

                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                EventRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Create(new EsportshubEventDto());
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(35)]
            [InlineData(30000)]
            [InlineData(100000)]
            public async void ReturnsObjectResultIfValiddDataIsNotSavedTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventId = id;
                EventRepository.Setup(x => x.Insert(instance));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Create(new EsportshubEventDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(35)]
            [InlineData(30000)]
            [InlineData(100000)]
            public async void ReturnsBadRequestTypeIfGameEventDtoIsNullTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventId = id;
                EventRepository.Setup(x => x.Insert(instance));
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                EsportshubEventDto esportshubEventDto =  null;

                var result = await EsportshubEventController.Create(esportshubEventDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IfCreatedAtRouteIsCreatedWithCorrectValuesWhenAValidEsportshubEventIsCreatedTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventId = id;
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                EventRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Create(new EsportshubEventDto()) as CreatedAtRouteResult;
                var routeValue = (int) result.RouteValues["Id"];
                Assert.Equal(routeValue, id);
            }

            [Fact]
            public async void IfCreatedAtRouteObjectIsEventWhenAValidGameEventIsSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                EventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                EventRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Create(new EsportshubEventDto()) as CreatedAtRouteResult;
                var routeObject = result.Value as EsportshubEventDto;
                Assert.IsType<EsportshubEventDto>(routeObject);
            }
        }

        public class GetEsportshubEventTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void ReturnJsonAsResultWithIdInputTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);

                EventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEvent>())).Returns(new EsportshubEvent());

                var result = await EsportshubEventController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnJsonAsResultTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await EsportshubEventController.Get();
                Assert.IsType<JsonResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IsTypeOfEventDtoTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                Mapper.Setup(m => m.Map<EsportshubEventDto>(It.IsAny<EsportshubEvent>())).Returns(new EsportshubEventDto());

                var result = await EsportshubEventController.Get(id) as JsonResult;
                var teamDto = result.Value as EsportshubEventDto;
                Assert.IsType<EsportshubEventDto>(teamDto);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-50)]
            [InlineData(-100000)]
            public async void InvalidInputTest(int id)
            {
                MockExtensions.ResetAll(Mocks());
                var result = await EsportshubEventController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}