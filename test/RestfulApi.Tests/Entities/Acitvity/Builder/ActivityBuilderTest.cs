using RestfulApi.Models.Esportshub.Entities.Activity;
using Xunit;
namespace RestfulApi.Tests.Entities.Acitvity
{
    public class ActivityBuilderTest
    {
        [Fact]
        public void TestActivityBuilderSetTitle()
        {
        //Given
        var title = "Fun With Me";
        var act = Activity.Builder()
        .SetTitle("hej")
        .Build();
        //When
        
        //Then
        Assert.Equal(act.Title,title);
        }
    }
}