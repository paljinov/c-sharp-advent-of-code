using System;
using System.Reflection;
using App.Tasks.Year2018.Day6;
using Xunit;

namespace Tests.Tasks.Year2018.Day6
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
            task.GetType()
                .GetField("totalDistanceToAllCoordinatesLessThan", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 32);
        }

        [Fact]
        public void Solution_CoordinatesExample_RegionSizeEquals()
        {
            string coordinates = "1, 1"
                + $"{Environment.NewLine}1, 6"
                + $"{Environment.NewLine}8, 3"
                + $"{Environment.NewLine}3, 4"
                + $"{Environment.NewLine}5, 5"
                + $"{Environment.NewLine}8, 9";

            Assert.Equal(16, task.Solution(coordinates));
        }
    }
}
