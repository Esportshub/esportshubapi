using Xunit;
using Moq;

namespace RestfulApi.Controllers
{
    public class PlayerControllerTest
    {
        [Fact]
        public void PostTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void GetTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

         [Fact]
        public void PutTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

         [Fact]
        public void DeleteTest()
        {
            Assert.Equal(5, Add(2, 2));
        }
    }
}