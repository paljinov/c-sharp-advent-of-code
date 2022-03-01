using System;
using App.Tasks.Year2018.Day21;
using Xunit;

namespace Tests.Tasks.Year2018.Day21
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_LowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithFewestInstructionsExecuted()
        {
            string instructions = "#ip 2"
                + $"{Environment.NewLine}seti 123 0 3"
                + $"{Environment.NewLine}bani 3 456 3"
                + $"{Environment.NewLine}eqri 3 72 3"
                + $"{Environment.NewLine}addr 3 2 2"
                + $"{Environment.NewLine}seti 0 0 2"
                + $"{Environment.NewLine}seti 0 6 3"
                + $"{Environment.NewLine}bori 3 65536 4"
                + $"{Environment.NewLine}seti 2176960 8 3"
                + $"{Environment.NewLine}bani 4 255 1"
                + $"{Environment.NewLine}addr 3 1 3"
                + $"{Environment.NewLine}bani 3 16777215 3"
                + $"{Environment.NewLine}muli 3 65899 3"
                + $"{Environment.NewLine}bani 3 16777215 3"
                + $"{Environment.NewLine}gtir 256 4 1"
                + $"{Environment.NewLine}addr 1 2 2"
                + $"{Environment.NewLine}addi 2 1 2"
                + $"{Environment.NewLine}seti 27 7 2"
                + $"{Environment.NewLine}seti 0 9 1"
                + $"{Environment.NewLine}addi 1 1 5"
                + $"{Environment.NewLine}muli 5 256 5"
                + $"{Environment.NewLine}gtrr 5 4 5"
                + $"{Environment.NewLine}addr 5 2 2"
                + $"{Environment.NewLine}addi 2 1 2"
                + $"{Environment.NewLine}seti 25 7 2"
                + $"{Environment.NewLine}addi 1 1 1"
                + $"{Environment.NewLine}seti 17 2 2"
                + $"{Environment.NewLine}setr 1 7 4"
                + $"{Environment.NewLine}seti 7 9 2"
                + $"{Environment.NewLine}eqrr 3 0 1"
                + $"{Environment.NewLine}addr 1 2 2"
                + $"{Environment.NewLine}seti 5 9 2";

            Assert.Equal(11474091, task.Solution(instructions));
        }
    }
}
