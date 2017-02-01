using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Integrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App.Controllers;
using RestfulApi.App.Dtos.IntegrationsDtos;
using Xunit;

namespace Test.RestfulApi.Test.Controllers
{
    public class IntegrationControllerTest
    {
        private static readonly Mock<IIntegrationRepository> IntegrationRepository = new Mock<IIntegrationRepository>();

        private static readonly Mock<ILogger<IntegrationController>> Logger =
            new Mock<ILogger<IntegrationController>>();

        private static readonly Mock<IMapper> Mapper = new Mock<IMapper>();

        private static readonly IntegrationController IntegrationController =
            new IntegrationController(IntegrationRepository.Object, Logger.Object, Mapper.Object);


        public static List<Mock> Mocks()
        {
            return new List<Mock>
            {
                IntegrationRepository,
                Logger,
                Mapper
            };
        }

        public class DeleteIntegrationTest
        {
            [Fact]
            public async void ReturnsNotFoundResultIfIntegrationDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await IntegrationController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void ReturnsNoContentResultIfAIntegrationIsDeletedWhenIdIsValid()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                instance.IntegrationGuid = id;
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Delete(id));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await IntegrationController.Delete(id);
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async void GetObjectResultIfIntegrationIsNotDeletedWithValidTeamId()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Delete(id));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await IntegrationController.Delete(id);
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void GetObjectResultWithStatusCode500IfIntegrationIsNotDeletedWithValidTeamId()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Delete(id));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await IntegrationController.Delete(id) as ObjectResult;
                Assert.NotNull(result);
                Assert.NotNull(result.StatusCode);
                Assert.Equal(500, result.StatusCode.Value );
            }
        }

        public class UpdateIntegrationTest
        {
            [Fact]
            public async void ReturnsBadRequestResultTypeIfIntegrationDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await IntegrationController.Update(new Guid(), null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void ReturnsNotFoundIfIntegrationDoesntExistTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await IntegrationController.Update(id, new IntegrationDto() { IntegrationGuid = id });

                Assert.IsType<NotFoundResult>(result);
            }


            [Fact]
            public async void ReturnsObjectResultIfIntegrationIsNotUpdatedWithValidIntegrationDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Update(instance));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await IntegrationController.Update(id, new IntegrationDto() { IntegrationGuid = id });
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultWithStatusCode500IfIntegrationIsNotUpdatedWithValidIntegrationDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Update(instance));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await IntegrationController.Update(id, new IntegrationDto() {IntegrationGuid = id}) as ObjectResult;
                Assert.NotNull(result);
                Assert.NotNull(result.StatusCode);
                Assert.Equal(500, result.StatusCode.Value);
            }

            [Fact]
            public async void GetNoContentResultIfAIntegrationIsUpdatedWithValidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Update(instance));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await IntegrationController.Update(id, new IntegrationDto() { IntegrationGuid = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostIntegrationTest
        {
            [Fact]
            public async void ReturnsCreatedAtRouteResultIfDataIsValidTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);

                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                IntegrationRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Integration>(It.IsAny<IntegrationDto>())).Returns(instance);

                var result = await IntegrationController.Create(new IntegrationDto());
                Assert.IsType<CreatedAtRouteResult>(result);
            }

            [Fact]
            public async void ReturnsObjectResultIfValidIntegrationDtoIsNotSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                IntegrationRepository.Setup(x => x.Insert(instance));

                var result = await IntegrationController.Create(new IntegrationDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void ReturnsBadRequestResultTypeIfIntegrationDtoIsNullTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await IntegrationController.Create(null);

                Assert.IsType<BadRequestResult>(result);
            }

            [Fact]
            public async void IfCreatedAtRouteIsCreatedWithRightValuesWhenAValidIntegrationIsCreatedTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                instance.IntegrationGuid = id;
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                IntegrationRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Integration>(It.IsAny<IntegrationDto>())).Returns(instance);

                var result = await IntegrationController.Create(new IntegrationDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                Guid guid;
                Assert.True(Guid.TryParse((string) result.RouteValues["Id"], out guid));
                Assert.Equal(id, guid);
            }

            [Fact]
            public async void IfCreatedAtRouteObjectIsIntegrationDtoWhenAValidIntegrationIsSavedTest()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                IntegrationRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Integration>(It.IsAny<IntegrationDto>())).Returns(instance);

                var result = await IntegrationController.Create(new IntegrationDto()) as CreatedAtRouteResult;
                Assert.NotNull(result);
                var routeObject = result.Value as IntegrationDto;
                Assert.IsType<IntegrationDto>(routeObject);
            }
        }

        public class GetIntegrationTest
        {
            [Fact]
            public async void ReturnJsonAsResultWithIdInputTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<IntegrationDto>(It.IsAny<Integration>())).Returns(new IntegrationDto());

                var result = await IntegrationController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }


            [Fact]
            public async void IsTypeOfIntegrationDtoTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = new Guid();

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);

                Mapper.Setup(m => m.Map<IntegrationDto>(It.IsAny<Integration>())).Returns(new IntegrationDto());

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                var result = await IntegrationController.Get(id) as JsonResult;
                Assert.NotNull(result);
                var teamDto = result.Value as IntegrationDto;
                Assert.IsType<IntegrationDto>(teamDto);
            }

            [Fact]
            public async void ReturnsBadRequestObjectResultWhenInvalidIdTest()
            {
                MockExtensions.ResetAll(Mocks());
                var id = Guid.Empty;

                var result = await IntegrationController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        public class GetIntegrationsTest
        {
            private IEnumerable<Integration> GetIntegrations(Guid[] integrationIds)
            {
                IEnumerable<Integration> integrations = new List<Integration>();
                foreach (var integrationId in integrationIds)
                {
                    var integration = (Integration) Activator.CreateInstance(typeof(Integration), true);
                    integration.IntegrationGuid = integrationId;
                    integrations.Append(integration);
                }
                return integrations;
            }

            [Fact]
            public async void ReturnsJsonResultWhenItFindsSomethingTest()
            {

                MockExtensions.ResetAll(Mocks());
                var integrationIds = new[] { new Guid(), new Guid(), new Guid(), new Guid() };
                var integrations = GetIntegrations(integrationIds);

                IntegrationRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Integration, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(integrations);
                foreach (var integrationId in integrationIds)
                {
                    var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                    instance.IntegrationGuid = integrationId;
                    var integrationDto = new IntegrationDto {IntegrationGuid = integrationId};
                    Mapper.Setup(x => x.Map<IntegrationDto>(instance)).Returns(integrationDto);
                }

                var integrationDtos = await IntegrationController.Get() as JsonResult;
                Assert.IsType<JsonResult>(integrationDtos);
            }

            [Fact]
            public async void ReturnsJsonResultWithIEnumerableIntegrationDtoAsValueWhenItFindsSomethingTest()
            {
                MockExtensions.ResetAll(Mocks());
                var integrationIds = new[] { new Guid(), new Guid(), new Guid(), new Guid() };
                var integrations = GetIntegrations(integrationIds);

                IntegrationRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Integration, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(integrations);
                foreach (var integrationId in integrationIds)
                {
                    var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                    instance.IntegrationGuid = integrationId;
                    var integrationDto = new IntegrationDto {IntegrationGuid = integrationId};
                    Mapper.Setup(x => x.Map<IntegrationDto>(instance)).Returns(integrationDto);
                }

                var result = await IntegrationController.Get() as JsonResult;
                Assert.NotNull(result);
                var integrationDtos = result.Value as IEnumerable<IntegrationDto>;
                Assert.NotNull(integrationDtos);

                foreach (var integrationDto in integrationDtos)
                {
                    Assert.True(integrationIds.Contains(integrationDto.IntegrationGuid));
                }
            }

            [Fact]
            public async void ReturnsNotFoundResultWhenItDoesntFindAnythingTest(IEnumerable<int> integrationIds)
            {
                IntegrationRepository.Setup(
                        x => x.FindByAsync(It.IsAny<Expression<Func<Integration, bool>>>(), It.IsAny<string>()))
                    .ReturnsAsync(null);

                var result = await IntegrationController.Get() as NotFoundResult;
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}

