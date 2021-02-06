using System;
using App.Tasks.Year2016.Day12;
using Xunit;

namespace Tests.Tasks.Year2016.Day12
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_RegisterAValueEquals()
        {
            string instructions = "cpy 41 a"
                + $"{Environment.NewLine}inc a"
                + $"{Environment.NewLine}inc a"
                + $"{Environment.NewLine}dec a"
                + $"{Environment.NewLine}jnz a 2"
                + $"{Environment.NewLine}dec a";

            Assert.Equal(42, task.Solution(instructions));
        }
    }
}
