using System;
using App.Tasks.Year2016.Day23;
using Xunit;

namespace Tests.Tasks.Year2016.Day23
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_RegisterAValueEquals()
        {
            string instructions = "cpy 2 a"
                + $"{Environment.NewLine}cpy 2 a"
                + $"{Environment.NewLine}tgl a"
                + $"{Environment.NewLine}tgl a"
                + $"{Environment.NewLine}tgl a"
                + $"{Environment.NewLine}cpy 1 a"
                + $"{Environment.NewLine}dec a"
                + $"{Environment.NewLine}dec a";

            Assert.Equal(3, task.Solution(instructions));
        }
    }
}
