using Data.App.Models;
using Data.App.Models.Entities;
using Data.App.Models.Repositories;
using Data.App.Models.Repositories.Activities;
using Data.App.Models.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RestfulApi.App.Controllers;
using Xunit;

namespace Test.RestfulApi.Test.Repositories
{
    public class PlayerRepositoryTest
    {
        public class FindByAsyncTest
        {
            private readonly Mock<IRepository<Player>> _internalPlayerRepository = new Mock<IRepository<Player>>();
            private readonly Mock<EsportshubContext> _esportshubContext = new Mock<EsportshubContext>();



            [Theory]
            [InlineData(1)]
            [InlineData(22)]
            [InlineData(5032)]
            [InlineData(100000)]
            public async void GetNotFoundIfActivityDosentExist(int id)
            {
                IPlayerRepository playerRepository = new PlayerRepository(_esportshubContext.Object, _internalPlayerRepository.Object);

                var result = await playerRepository.(id);

                Assert.IsType<NotFoundResult>(result);
            }



        }

        public class FindAsyncTest
        {

        }

        public class SaveAsyncTest
        {

        }

        public class InsertTest
        {

        }

        public class DeleteTest
        {

        }

        public class UpdateTest
        {

        }

        public class SaveTest
        {

        }

    }
}