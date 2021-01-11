using System;
using App.Tasks.Year2016.Day8;
using Xunit;

namespace Tests.Tasks.Year2016.Day8
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_LitPixelsEquals()
        {
            string instructions = "rect 3x2"
              + $"{Environment.NewLine}rotate column x=1 by 1"
              + $"{Environment.NewLine}rotate row y=0 by 4"
              + $"{Environment.NewLine}rotate column x=1 by 1";

            Assert.Equal(6, task.Solution(instructions));
        }
    }
}
