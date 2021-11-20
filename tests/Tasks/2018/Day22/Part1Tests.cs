using System;
using App.Tasks.Year2018.Day22;
using Xunit;

namespace Tests.Tasks.Year2018.Day22
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ScanExample_TotalRiskLevelForTheSmallestRectangleThatIncludesCaveMouthAndTargetCoordinatesEquals()
        {
            string scan = "depth: 510"
                + $"{Environment.NewLine}target: 10,10";

            Assert.Equal(114, task.Solution(scan));
        }
    }
}
