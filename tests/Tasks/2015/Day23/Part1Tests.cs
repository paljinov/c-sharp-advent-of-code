using System;
using App.Tasks.Year2015.Day23;
using Xunit;

namespace Tests.Tasks.Year2015.Day23
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_RegisterAValueWhenTheProgramIsFinishedExecutingEquals()
        {
            string instructions = "inc a"
                + $"{Environment.NewLine}jio a, +2"
                + $"{Environment.NewLine}tpl a"
                + $"{Environment.NewLine}inc a";

            instructions = instructions.Replace('a', 'b');

            Assert.Equal(2, task.Solution(instructions));
        }
    }
}
