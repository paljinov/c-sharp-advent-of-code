using System;
using App.Tasks.Year2021.Day9;
using Xunit;

namespace Tests.Tasks.Year2021.Day9
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_HeightmapExample_RiskLevelsSumOfAllHeightmapLowPointsEquals()
        {
            string heightmap = "2199943210"
                + $"{Environment.NewLine}3987894921"
                + $"{Environment.NewLine}9856789892"
                + $"{Environment.NewLine}8767896789"
                + $"{Environment.NewLine}9899965678";

            Assert.Equal(15, task.Solution(heightmap));
        }
    }
}
