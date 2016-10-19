using Xunit;
using esportshubapi.Controllers;
using esportshubapi.Repositories;
using esportshubapi.Models;
namespace RestfulAPI.Tests.Entities
{
    public class Game
    {
       [Fact]
        public void GameBuilder()
        {
            var game = Game.Builder();
            
        }
    
    }
}