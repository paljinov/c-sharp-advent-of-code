using System;
using App.Tasks.Year2015.Day9;
using Xunit;

namespace Tests.Tasks.Year2015.Day9
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ExampleDistances_ShortestRouteDistanceEquals()
        {
            string distances = "London to Dublin = 464"
                + $"{Environment.NewLine}London to Belfast = 518"
                + $"{Environment.NewLine}Dublin to Belfast = 141";

            Assert.Equal(605, task.Solution(distances));
        }
    }
}
