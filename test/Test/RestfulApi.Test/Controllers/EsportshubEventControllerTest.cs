using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        private static readonly Mock<IEsportshubEventRepository> EsportshubEventRepository = new Mock<IEsportshubEventRepository>();
        private static readonly Mock<ILogger<EsportshubEventController>> Logger = new Mock<ILogger<EsportshubEventController>>();
        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        private static readonly EsportshubEventController EsportshubEventController =
            new EsportshubEventController(EsportshubEventRepository.Object, Logger.Object, Mapper.Object);


        public static List<Mock> Mocks()
        {
            return new List<Mock>
            {
                EsportshubEventRepository,
                Logger,
                Mapper
            };
        }


        public class DeleteEsportshubEventTest
        {
            [Fact]
            public async void ReturnsNotFoundIfEsportshubEventDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                EsportshubEventRepository.Setup(x => x.Delete(id));
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfAEsportshubEventIsDeletedWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (EsportshubEvent) Activator.CreateInstance(type: typeof(EsportshubEvent), nonPublic: true);
                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EsportshubEventRepository.Setup(x => x.Delete(id));
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await EsportshubEventController.Delete(id);
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfDataIsNotDeletedWithValidEsportshubEventIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                var id = Guid.NewGuid();
                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EsportshubEventRepository.Setup(x => x.Delete(id));
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Delete(id);
                Assert.IsType<ObjectResult>(result);
            }
        }

        public class UpdateEsportshubEventTest
        {
            [Fact]
            public async void ReturnsBadRequestResultTypeIfEsportshubEventDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await EsportshubEventController.Update(new Guid(), null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsNotFoundIfEsportshubEventDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                EsportshubEventRepository.Setup(x => x.Update(instance));
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Update(id, new EsportshubEventDto() { EsportshubEventGuid = id });

                Assert.IsType<NotFoundResult>(result);
            }


            [Fact]
            public async void ReturnsObjectResultIfEsportshubEventIsNotUpdatedWithValidEventDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);

                instance.EsportshubEventGuid = id;
                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EsportshubEventRepository.Setup(x => x.Update(instance));
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await EsportshubEventController.Update(id, new EsportshubEventDto() { EsportshubEventGuid = id });
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfAEsportshubEventIsUpdatedWithValidDataTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();
                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventGuid = id;
                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                EsportshubEventRepository.Setup(x => x.Update(instance));
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Update(id, new EsportshubEventDto() { EsportshubEventGuid = id });
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostEsportshubEventTest
        {
            [Fact]
            public async void ReturnsCreateAtRouteResultIfDataIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventGuid = id;

                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                EsportshubEventRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Create(new EsportshubEventDto());
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfValiddDataIsNotSavedTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventGuid = id;
                EsportshubEventRepository.Setup(x => x.Insert(instance));
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Create(new EsportshubEventDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsBadRequestTypeIfEsportshubEventDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await EsportshubEventController.Create(null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void IfCreatedAtRouteResultIsCreatedWithCorrectValuesWhenAValidEsportshubEventIsCreatedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                var id = Guid.NewGuid();
                instance.EsportshubEventGuid = id;
                var esportshubEventDto = new EsportshubEventDto {EsportshubEventGuid = id};
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                EsportshubEventRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<EsportshubEvent>(esportshubEventDto)).Returns(instance);

                var result = await EsportshubEventController.Create(esportshubEventDto) as CreatedAtRouteResult;
                Assert.NotNull(result);
                object ok;
                Assert.True(result.RouteValues.TryGetValue("Id", out ok));
            }

            [Fact]
            public async void IfCreatedAtRouteObjectIsEventWhenAValidGameEventIsSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                var id = Guid.NewGuid();
                instance.EsportshubEventGuid = id;
                EsportshubEventRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                EsportshubEventRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<EsportshubEvent>(It.IsAny<EsportshubEventDto>())).Returns(instance);

                var result = await EsportshubEventController.Create(new EsportshubEventDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                var routeObject = result.Value as EsportshubEventDto;
                Assert.IsType<EsportshubEventDto>(routeObject);
            }
        }

        public class GetEsportshubEventTest
        {
            [Fact]
            public async void ReturnJsonAsResultWithValidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventGuid = id;

                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                var esportshubEventDto = new EsportshubEventDto {EsportshubEventGuid = id};
                Mapper.Setup(m => m.Map<EsportshubEventDto>(It.IsAny<EsportshubEvent>())).Returns(esportshubEventDto);

                var result = await EsportshubEventController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnsJsonResultWithValueOfTypeOfEsportshubEventDtoWhenValidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.NewGuid();

                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventGuid = id;
                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                var esportshubEventDto = new EsportshubEventDto {EsportshubEventGuid = id};
                Mapper.Setup(m => m.Map<EsportshubEventDto>(It.IsAny<EsportshubEvent>())).Returns(esportshubEventDto);

                var result = await EsportshubEventController.Get(id) as JsonResult;
                Assert.NotNull(result);
                var teamDto = result.Value as EsportshubEventDto;
                Assert.IsType<EsportshubEventDto>(teamDto);
            }

            [Fact]
            public async void ReturnsBadRequestObjectResultWhenInvalidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.Empty;
                var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                instance.EsportshubEventGuid = id;
                EsportshubEventRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var esportshubEventDto = new EsportshubEventDto {EsportshubEventGuid = id};
                Mapper.Setup(m => m.Map<EsportshubEventDto>(It.IsAny<EsportshubEvent>())).Returns(esportshubEventDto);

                var result = await EsportshubEventController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        public class GetEsportshubEventsTest
        {
            private IEnumerable<EsportshubEvent> GetEsportshubEvents(Guid[] esportshubEventIds)
            {
                List<EsportshubEvent> esportshubEvents = new List<EsportshubEvent>();
                foreach (var esportshubEventId in esportshubEventIds)
                {
                    var esportshubEvent = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), true);
                    esportshubEvent.EsportshubEventGuid = esportshubEventId;
                    esportshubEvents.Add(esportshubEvent);
                }
                return esportshubEvents;
            }

            [Fact]
            public async void ReturnsJsonResultWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var esportshubEventIds = new[] {new Guid(), Guid.NewGuid(), Guid.NewGuid()};
                var esportshubEvents = GetEsportshubEvents(esportshubEventIds);

                EsportshubEventRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<EsportshubEvent, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(esportshubEvents);
                foreach (var esportshubEventId in esportshubEventIds)
                {
                    var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                    instance.EsportshubEventGuid = esportshubEventId;
                    var esportshubEventDto = new EsportshubEventDto {EsportshubEventGuid = esportshubEventId};
                    Mapper.Setup(x => x.Map<EsportshubEventDto>(instance)).Returns(esportshubEventDto);
                }

                var esportshubEventDtos = await EsportshubEventController.Get() as JsonResult;
                Assert.IsType<JsonResult>(esportshubEventDtos);
            }

            [Fact]
            public async void ReturnsJsonResultWithIEnumerableEsportshubEventDtoAsValueWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var esportshubEventIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
                var esportshubEvents = GetEsportshubEvents(esportshubEventIds);

                EsportshubEventRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<EsportshubEvent, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(esportshubEvents);

                foreach (var esportshubEvent in esportshubEvents)
                {
                    var esportshubEventDto = new EsportshubEventDto {EsportshubEventGuid = esportshubEvent.EsportshubEventGuid};
                    Mapper.Setup(x => x.Map<EsportshubEventDto>(esportshubEvent)).Returns(esportshubEventDto);
                }

                var result = await EsportshubEventController.Get() as JsonResult;
                Assert.NotNull(result);
                var esportshubEventDtos = result.Value as EsportshubEventDto[];

                Assert.NotNull(esportshubEventDtos);
                foreach (var esportshubEventDto in esportshubEventDtos)
                {
                    Assert.True(esportshubEventIds.Contains(esportshubEventDto.EsportshubEventGuid));
                }
            }

            [Fact]
            public async void ReturnsNotFoundResultWhenItDoesntFindAnythingTest()
            {

                MockExtensions.ResetAll(Mocks());
                var esportshubEventIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
                EsportshubEventRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<EsportshubEvent, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(null);
                foreach (var esportshubEventId in esportshubEventIds)
                {
                    var instance = (EsportshubEvent) Activator.CreateInstance(typeof(EsportshubEvent), nonPublic: true);
                    instance.EsportshubEventGuid = esportshubEventId;
                    var esportshubEventDto = new EsportshubEventDto {EsportshubEventGuid = esportshubEventId};
                    Mapper.Setup(x => x.Map<EsportshubEventDto>(instance)).Returns(esportshubEventDto);
                }

                var result = await EsportshubEventController.Get() as NotFoundResult;
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}