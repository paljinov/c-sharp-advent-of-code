using System;
using App.Tasks.Year2017.Day8;
using Xunit;

namespace Tests.Tasks.Year2017.Day8
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_HighestValueHeldInAnyRegisterDuringComputationProcessEquals()
        {
            string instructions = $"{Environment.NewLine}b inc 5 if a > 1"
                + $"{Environment.NewLine}a inc 1 if b < 5"
                + $"{Environment.NewLine}c dec -10 if a >= 1"
                + $"{Environment.NewLine}c inc -20 if c == 10";

            Assert.Equal(10, task.Solution(instructions));
        }
    }
}
