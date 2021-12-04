using System;
using App.Tasks.Year2021.Day2;
using Xunit;

namespace Tests.Tasks.Year2021.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_ProductOfFinalHorizontalPositionAndDepthEquals()
        {
            string instructions = "forward 5"
                + $"{Environment.NewLine}down 5"
                + $"{Environment.NewLine}forward 8"
                + $"{Environment.NewLine}up 3"
                + $"{Environment.NewLine}down 8"
                + $"{Environment.NewLine}forward 2";

            Assert.Equal(150, task.Solution(instructions));
        }
    }
}
