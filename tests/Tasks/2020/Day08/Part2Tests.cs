using System;
using App.Tasks.Year2020.Day8;
using Xunit;

namespace Tests.Tasks.Year2020.Day8
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_AccumulatorValueAfterProgramTerminatesEquals()
        {
            string instructions = "nop +0"
                + $"{Environment.NewLine}acc +1"
                + $"{Environment.NewLine}jmp +4"
                + $"{Environment.NewLine}acc +3"
                + $"{Environment.NewLine}jmp -3"
                + $"{Environment.NewLine}acc -99"
                + $"{Environment.NewLine}acc +1"
                + $"{Environment.NewLine}jmp -4"
                + $"{Environment.NewLine}acc +6";

            Assert.Equal(8, task.Solution(instructions));
        }
    }
}
