using System.Reflection;
using App.Tasks.Year2016.Day13;
using Xunit;

namespace Tests.Tasks.Year2016.Day13
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            task.GetType()
                .GetField("mostSteps", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 10);
        }

        [Fact]
        public void Solution_FavoriteNumberExample_DifferentLocationsVisitedAtMostTenStepsEquals()
        {
            string favoriteNumber = "10";
            Assert.Equal(18, task.Solution(favoriteNumber));
        }
    }
}
