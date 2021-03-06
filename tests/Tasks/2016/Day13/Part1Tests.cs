using System.Reflection;
using App.Tasks.Year2016.Day13;
using Xunit;

namespace Tests.Tasks.Year2016.Day13
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            task.GetType()
                .GetField("destinationX", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 7);
            task.GetType()
                .GetField("destinationY", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 4);
        }

        [Fact]
        public void Solution_FavoriteNumberExample_FewestNumberOfStepsToReachCoordinatesEquals()
        {
            string favoriteNumber = "10";
            Assert.Equal(11, task.Solution(favoriteNumber));
        }
    }
}
