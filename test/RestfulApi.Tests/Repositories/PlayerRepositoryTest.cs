using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace RestfulApi.Tests.Repositories
{
    public class PlayerRepositoryTest : IDisposable
    {
        private Mock<IPlayerRepository> _playerRepository;
        public PlayerRepositoryTest() 
        {
            _playerRepository = new Mock<IPlayerRepository>();
            _playerRepository.SetupGet(repo => repo.GetAsync(null, "")); 
           // _playerRepository.SetupGet(repo => repo.GetById(player.PlayerId));
        }

        private static DbContextOptions<EsportshubContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EsportshubContext>();
            builder.UseInMemoryDatabase().UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        /**[Fact]*/
         public void PostTest()
         {
             Player player = new Player();
//             _playerRepository.SetupGet(repo => repo.InsertAsync(player));
             
            
           
            //mockRepo.Setup(repo => repo.Insert(player)).Returns(true);
            //mockRepo.Setup(repo => repo.Insert(player)).Returns(Task.FromResult((Player) null));
          //   var PlayerController = new PlayerController(mockRepo);
          //   Assert.Equal(4, Add(2, 2));
         }

        void IDisposable.Dispose()
        {
           _playerRepository = null;
        }
    }
}