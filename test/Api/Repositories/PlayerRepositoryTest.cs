using Xunit;
using Moq;

namespace Api.Repositories
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
        public void GetTest()
        {
            Assert.Equal(5, Add(2, 2));
        }
    }
}