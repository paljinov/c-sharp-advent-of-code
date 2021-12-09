using System;
using App.Tasks.Year2021.Day9;
using Xunit;

namespace Tests.Tasks.Year2021.Day9
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_HeightmapExample_ProductOfThreeLargestBasinsSizesEquals()
        {
            string heightmap = "2199943210"
                + $"{Environment.NewLine}3987894921"
                + $"{Environment.NewLine}9856789892"
                + $"{Environment.NewLine}8767896789"
                + $"{Environment.NewLine}9899965678";

            Assert.Equal(1134, task.Solution(heightmap));
        }
    }
}
