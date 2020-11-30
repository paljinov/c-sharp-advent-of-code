using System;
using App.Tasks.Year2015.Day9;
using Xunit;

namespace Tests.Tasks.Year2015.Day9
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ExampleDistances_LongestRouteDistanceEquals()
        {
            string distances = "London to Dublin = 464"
                + $"{Environment.NewLine}London to Belfast = 518"
                + $"{Environment.NewLine}Dublin to Belfast = 141";

            Assert.Equal(982, task.Solution(distances));
        }
    }
}
