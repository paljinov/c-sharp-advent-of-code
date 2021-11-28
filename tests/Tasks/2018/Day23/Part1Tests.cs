using System;
using App.Tasks.Year2018.Day23;
using Xunit;

namespace Tests.Tasks.Year2018.Day23
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_NanobotsExample_NanobotsWhichAreInRangeOfTheLargestSignalRadiusCountEquals()
        {
            string nanobots = "pos=<0,0,0>, r=4"
                + $"{Environment.NewLine}pos=<1,0,0>, r=1"
                + $"{Environment.NewLine}pos=<4,0,0>, r=3"
                + $"{Environment.NewLine}pos=<0,2,0>, r=1"
                + $"{Environment.NewLine}pos=<0,5,0>, r=3"
                + $"{Environment.NewLine}pos=<0,0,3>, r=1"
                + $"{Environment.NewLine}pos=<1,1,1>, r=1"
                + $"{Environment.NewLine}pos=<1,1,2>, r=1"
                + $"{Environment.NewLine}pos=<1,3,1>, r=1";

            Assert.Equal(7, task.Solution(nanobots));
        }
    }
}
