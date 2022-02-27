using System;
using App.Tasks.Year2018.Day21;
using Xunit;

namespace Tests.Tasks.Year2018.Day21
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_LowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithMostInstructionsExecuted()
        {
            string instructions = "#ip 4"
                + $"{Environment.NewLine}addi 4 16 4"
                + $"{Environment.NewLine}seti 1 9 5"
                + $"{Environment.NewLine}seti 1 5 2"
                + $"{Environment.NewLine}mulr 5 2 1"
                + $"{Environment.NewLine}eqrr 1 3 1"
                + $"{Environment.NewLine}addr 1 4 4"
                + $"{Environment.NewLine}addi 4 1 4"
                + $"{Environment.NewLine}addr 5 0 0"
                + $"{Environment.NewLine}addi 2 1 2"
                + $"{Environment.NewLine}gtrr 2 3 1"
                + $"{Environment.NewLine}addr 4 1 4"
                + $"{Environment.NewLine}seti 2 6 4"
                + $"{Environment.NewLine}addi 5 1 5"
                + $"{Environment.NewLine}gtrr 5 3 1"
                + $"{Environment.NewLine}addr 1 4 4"
                + $"{Environment.NewLine}seti 1 2 4"
                + $"{Environment.NewLine}mulr 4 4 4"
                + $"{Environment.NewLine}addi 3 2 3"
                + $"{Environment.NewLine}mulr 3 3 3"
                + $"{Environment.NewLine}mulr 4 3 3"
                + $"{Environment.NewLine}muli 3 11 3"
                + $"{Environment.NewLine}addi 1 5 1"
                + $"{Environment.NewLine}mulr 1 4 1"
                + $"{Environment.NewLine}addi 1 2 1"
                + $"{Environment.NewLine}addr 3 1 3"
                + $"{Environment.NewLine}addr 4 0 4"
                + $"{Environment.NewLine}seti 0 2 4"
                + $"{Environment.NewLine}setr 4 8 1"
                + $"{Environment.NewLine}mulr 1 4 1"
                + $"{Environment.NewLine}addr 4 1 1"
                + $"{Environment.NewLine}mulr 4 1 1"
                + $"{Environment.NewLine}muli 1 14 1"
                + $"{Environment.NewLine}mulr 1 4 1"
                + $"{Environment.NewLine}addr 3 1 3"
                + $"{Environment.NewLine}seti 0 0 0"
                + $"{Environment.NewLine}seti 0 2 4";

            Assert.Equal(3650572, task.Solution(instructions));
        }
    }
}
