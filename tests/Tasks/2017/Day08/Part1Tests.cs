using System;
using App.Tasks.Year2017.Day8;
using Xunit;

namespace Tests.Tasks.Year2017.Day8
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_LargestValueInAnyRegisterEquals()
        {
            string instructions = $"{Environment.NewLine}b inc 5 if a > 1"
                + $"{Environment.NewLine}a inc 1 if b < 5"
                + $"{Environment.NewLine}c dec -10 if a >= 1"
                + $"{Environment.NewLine}c inc -20 if c == 10";

            Assert.Equal(1, task.Solution(instructions));
        }
    }
}
