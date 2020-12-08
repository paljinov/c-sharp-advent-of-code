using System;
using App.Tasks.Year2020.Day8;
using Xunit;

namespace Tests.Tasks.Year2020.Day8
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_AccumulatorValueBeforeAnyInstructionIsExecutedSecondTimeEquals()
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

            Assert.Equal(5, task.Solution(instructions));
        }
    }
}
