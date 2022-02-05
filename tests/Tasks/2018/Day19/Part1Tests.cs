using System;
using App.Tasks.Year2018.Day19;
using Xunit;

namespace Tests.Tasks.Year2018.Day19
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_RegisterZeroValueWhenTheBackgroundProcessHaltsEquals()
        {
            string instructions = "#ip 0"
                + $"{Environment.NewLine}seti 5 0 1"
                + $"{Environment.NewLine}seti 6 0 2"
                + $"{Environment.NewLine}addi 0 1 0"
                + $"{Environment.NewLine}addr 1 2 3"
                + $"{Environment.NewLine}setr 1 0 0"
                + $"{Environment.NewLine}seti 8 0 4"
                + $"{Environment.NewLine}seti 9 0 5";

            Assert.Equal(6, task.Solution(instructions));
        }
    }
}
