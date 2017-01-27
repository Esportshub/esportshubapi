using System;
using System.Collections.Generic;
using AutoMapper;
using Data.App.Models.Entities;
using Data.App.Models.Repositories.Integrations;
using Data.App.Models.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestfulApi.App.Controllers;
using RestfulApi.App.Dtos.IntegrationsDtos;
using RestfulApi.App.Dtos.TeamDtos;
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
            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNotFoundIfIntegrationDosentExist(int id)
            {
                MockExtensions.ResetAll(Mocks());

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);

                var result = await IntegrationController.Delete(id);

                Assert.IsType<NotFoundResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNoContentResultIfAIntegrationIsDeletedWithValidData(int id)
            {
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Delete(id));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await IntegrationController.Update(new IntegrationDto() {IntegrationId = id});
                Assert.IsType<NoContentResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetObjectResultIfIntegrationIsNotDeleteWithValidTeamId(int id)
            {
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Delete(id));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await IntegrationController.Delete(id);
                Assert.IsType<ObjectResult>(result);
            }
        }

        public class UpdateIntegrationTest
        {
            [Fact]
            public async void GetBadRequestTypeOfIntegrationDtoIsNull()
            {
                MockExtensions.ResetAll(Mocks());

                IntegrationDto integreationDto = null;

                var result = await IntegrationController.Update(integreationDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNotFoundIfIntegrationDosentExist(int id)
            {
                MockExtensions.ResetAll(Mocks());

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(null);
                var result = await IntegrationController.Update(new IntegrationDto() {IntegrationId = id});

                Assert.IsType<NotFoundResult>(result);
            }


            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetObjectResultIfDataIsNotUpdateWithValidIntegrationDto(int id)
            {
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Update(instance));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);

                var result = await IntegrationController.Update(new IntegrationDto() {IntegrationId = id});
                Assert.IsType<ObjectResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNoContentResultIfAIntegrationIsUpdateWithValidData(int id)
            {
                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                IntegrationRepository.Setup(x => x.Update(instance));
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);

                var result = await IntegrationController.Update(new IntegrationDto() {IntegrationId = id});
                Assert.IsType<NoContentResult>(result);
            }
        }

        public class PostIntegrationTest
        {
            [Fact]
            public async void GetCreateAtRouteResultIfDataIsValid()
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
            public async void GetObjectResultIfValiddDataIsNotSaved()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(false);
                IntegrationRepository.Setup(x => x.Insert(instance));

                var result = await IntegrationController.Create(new IntegrationDto());
                Assert.IsType<ObjectResult>(result);
            }

            [Fact]
            public async void GetBadRequestTypeIfIntegrationDtoIsNull()
            {
                MockExtensions.ResetAll(Mocks());

                IntegrationDto integrationDto = null;

                var result = await IntegrationController.Create(integrationDto);

                Assert.IsType<BadRequestResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void CheckIfCreateAtRouteIsCreatedWithRightValuesWhenAValidIntegrationIsCreated(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                instance.IntegrationId = id;
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                IntegrationRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Integration>(It.IsAny<IntegrationDto>())).Returns(instance);

                var result = await IntegrationController.Create(new IntegrationDto()) as CreatedAtRouteResult;
                var routeValue = (int) result.RouteValues["Id"];
                Assert.Equal(routeValue, id);
            }

            [Fact]
            public async void CheckIfCreatedAtRouteObjectIsTeamWhenAValidIntegrationIsSaved()
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);
                IntegrationRepository.Setup(x => x.SaveAsync()).ReturnsAsync(true);
                IntegrationRepository.Setup(x => x.Insert(instance));
                Mapper.Setup(m => m.Map<Integration>(It.IsAny<IntegrationDto>())).Returns(instance);

                var result = await IntegrationController.Create(new IntegrationDto()) as CreatedAtRouteResult;
                var routeObject = result.Value as IntegrationDto;
                Assert.IsType<IntegrationDto>(routeObject);
            }
        }

        public class GetIntegrationTest
        {
            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void ReturnJsonAsResultWithIdInputTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                Mapper.Setup(m => m.Map<IntegrationDto>(It.IsAny<Integration>())).Returns(new IntegrationDto());

                var result = await IntegrationController.Get(id) as JsonResult;
                Assert.IsType<JsonResult>(result);
            }

            [Fact]
            public async void ReturnJsonAsResultTest()
            {
                MockExtensions.ResetAll(Mocks());

                var result = await IntegrationController.Get();
                Assert.IsType<JsonResult>(result);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(37)]
            [InlineData(50000)]
            [InlineData(100000)]
            public async void IsTypeOfIntegrationDtoTest(int id)
            {
                MockExtensions.ResetAll(Mocks());

                var instance = (Integration) Activator.CreateInstance(typeof(Integration), nonPublic: true);

                Mapper.Setup(m => m.Map<IntegrationDto>(It.IsAny<Integration>())).Returns(new IntegrationDto());

                IntegrationRepository.Setup(x => x.FindAsync(id)).ReturnsAsync(instance);
                var result = await IntegrationController.Get(id) as JsonResult;
                var teamDto = result.Value as IntegrationDto;
                Assert.IsType<IntegrationDto>(teamDto);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-50)]
            [InlineData(-100000)]
            public async void InvalidInputTest(int id)
            {
                MockExtensions.ResetAll(Mocks());
                var result = await IntegrationController.Get(id) as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}

