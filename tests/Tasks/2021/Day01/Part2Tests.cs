using System;
using App.Tasks.Year2021.Day1;
using Xunit;

namespace Tests.Tasks.Year2021.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_DepthsExample_ThreeMeasurementSlidingWindowSumsWhichAreLargerThanThePreviousSumCountEquals()
        {
            string depths = "199"
                + $"{Environment.NewLine}200"
                + $"{Environment.NewLine}208"
                + $"{Environment.NewLine}210"
                + $"{Environment.NewLine}200"
                + $"{Environment.NewLine}207"
                + $"{Environment.NewLine}240"
                + $"{Environment.NewLine}269"
                + $"{Environment.NewLine}260"
                + $"{Environment.NewLine}263";

            Assert.Equal(5, task.Solution(depths));
        }
    }
}
