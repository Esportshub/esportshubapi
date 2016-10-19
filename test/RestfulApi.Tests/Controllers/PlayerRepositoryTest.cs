using Xunit;
using Moq;
using EsportshubApi.Controllers;
using EsportshubApi.Models.Repositories;
using EsportshubApi.Models;
using EsportshubApi.Models.Entities;



namespace RestfulApi.Test.Controllers
{
    public class PlayerControllerTest
    {
        [Fact]
         public void PostTest()
         {
             var mockRepo = new Mock<IPlayerRepository>();
             Player player = new Player();
            mockRepo.Setup(repo => repo.Insert(player)).;
            //mockRepo.Setup(repo => repo.Insert(player)).Returns(Task.FromResult((Player) null));
             var PlayerController = new PlayerController(mockRepo);
             Assert.Equal(4, Add(2, 2));             
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