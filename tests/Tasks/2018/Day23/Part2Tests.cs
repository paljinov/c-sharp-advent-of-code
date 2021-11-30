using System;
using App.Tasks.Year2018.Day23;
using Xunit;

namespace Tests.Tasks.Year2018.Day23
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_NanobotsExample_ShortestManhattanDistanceForPositionInRangeOfLargestNumberOfNanobotsEquals()
        {
            string nanobots = "pos=<10,12,12>, r=2"
                + $"{Environment.NewLine}pos=<12,14,12>, r=2"
                + $"{Environment.NewLine}pos=<16,12,12>, r=4"
                + $"{Environment.NewLine}pos=<14,14,14>, r=6"
                + $"{Environment.NewLine}pos=<50,50,50>, r=20"
                + $"{Environment.NewLine}pos=<10,10,10>, r=5";

            Assert.Equal(36, task.Solution(nanobots));
        }
    }
}
