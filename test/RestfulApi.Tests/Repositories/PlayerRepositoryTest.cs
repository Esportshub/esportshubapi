using System;
using Xunit;
using Moq;
using EsportshubApi.Models.Repositories;
using EsportshubApi.Models;
using EsportshubApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RestfulApi.Tests.Controllers
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
             _playerRepository.SetupGet(repo => repo.InsertAsync(player));
             
            
           
            //mockRepo.Setup(repo => repo.Insert(player)).Returns(true);
            //mockRepo.Setup(repo => repo.Insert(player)).Returns(Task.FromResult((Player) null));
          //   var PlayerController = new PlayerController(mockRepo);
          //   Assert.Equal(4, Add(2, 2));
         }

        void IDisposable.Dispose()
        {
           _playerRepository = null;
        }

        // [Fact]
        // public void GetTest()
        // {
        //     Assert.Equal(5, Add(2, 2));
        // }

        //  [Fact]
        // public void PutTest()
        // {
        //     Assert.Equal(5, Add(2, 2));
        // }

        //  [Fact]
        // public void DeleteTest()
        // {
        //     Assert.Equal(5, Add(2, 2));
        // }
    }
}