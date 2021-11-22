using System;
using App.Tasks.Year2018.Day22;
using Xunit;

namespace Tests.Tasks.Year2018.Day22
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ScanExample_FewestNumberOfMinutesNeededToReachTheTargetEquals()
        {
            string scan = "depth: 510"
                + $"{Environment.NewLine}target: 10,10";

            Assert.Equal(45, task.Solution(scan));
        }
    }
}
