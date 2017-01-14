using Xunit;

namespace Data.Test.Entities.Activities.Builders
{
    public class ActivityBuilderTest
    {
        [Fact]
        public void AcivityBuilderIsTypeOfActivity()
        {
            var act = Activity.Builder().Build();

            Assert.IsType<Activity>(act);
        }


        [Fact]
        public void ActivityBuilderSetTitle()
        {
            //Given
            const string title = "Fun With Me";
            var act = Activity.Builder()
                .SetTitle(title)
                .Build();
            //When

            //Then
            Assert.Equal(act.Title, title);
        }

        [Fact]
        public void ActivityBuilderSetDescription()
        {
            const string description = "This is the best activity in the world";
            var act = Activity.Builder()
                .SetDescription(description)
                .Build();

            Assert.Equal(act.Description, description);
        }
    }
}