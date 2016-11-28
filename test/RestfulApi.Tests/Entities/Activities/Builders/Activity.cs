using RestfulApi.App.Models.Esportshub.Entities;
using Xunit;

namespace RestfulApi.Tests.Entities.Activities.Builders
{
    public class ActivityBuilderTest
    {
        [Fact]
        public void TestActivityBuilderSetTitle()
        {
        //Given
        const string title = "Fun With Me";
        var act = Activity.Builder()
        .SetTitle(title)
        .Build();
        //When
        
        //Then
        Assert.Equal(act.Title,title);
        }

        [Fact]
        public void TestActivityBuilderSetDescription()
        {
            const string description = "This is the best activity in the world";
            var act = Activity.Builder()
                .SetDescription(description)
                .Build();

            Assert.Equal(act.Description, description);

        }
    }
}