using System;
using App.Tasks.Year2021.Day2;
using Xunit;

namespace Tests.Tasks.Year2021.Day2
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_ProductOfFinalHorizontalPositionAndDepthWhenTrackingAimEquals()
        {
            string instructions = "forward 5"
                + $"{Environment.NewLine}down 5"
                + $"{Environment.NewLine}forward 8"
                + $"{Environment.NewLine}up 3"
                + $"{Environment.NewLine}down 8"
                + $"{Environment.NewLine}forward 2";

            Assert.Equal(900, task.Solution(instructions));
        }
    }
}
