using System;
using App.Tasks.Year2016.Day25;
using Xunit;

namespace Tests.Tasks.Year2016.Day25
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_LowestRegisterAInitialPositiveIntegerThatCausesClockSignalEquals()
        {
            string instructions = "cpy a d"
                + $"{Environment.NewLine}cpy 4 c"
                + $"{Environment.NewLine}cpy 643 b"
                + $"{Environment.NewLine}inc d"
                + $"{Environment.NewLine}dec b"
                + $"{Environment.NewLine}jnz b -2"
                + $"{Environment.NewLine}dec c"
                + $"{Environment.NewLine}jnz c -5"
                + $"{Environment.NewLine}cpy d a"
                + $"{Environment.NewLine}jnz 0 0"
                + $"{Environment.NewLine}cpy a b"
                + $"{Environment.NewLine}cpy 0 a"
                + $"{Environment.NewLine}cpy 2 c"
                + $"{Environment.NewLine}jnz b 2"
                + $"{Environment.NewLine}jnz 1 6"
                + $"{Environment.NewLine}dec b"
                + $"{Environment.NewLine}dec c"
                + $"{Environment.NewLine}jnz c -4"
                + $"{Environment.NewLine}inc a"
                + $"{Environment.NewLine}jnz 1 -7"
                + $"{Environment.NewLine}cpy 2 b"
                + $"{Environment.NewLine}jnz c 2"
                + $"{Environment.NewLine}jnz 1 4"
                + $"{Environment.NewLine}dec b"
                + $"{Environment.NewLine}dec c"
                + $"{Environment.NewLine}jnz 1 -4"
                + $"{Environment.NewLine}jnz 0 0"
                + $"{Environment.NewLine}out b"
                + $"{Environment.NewLine}jnz a -19"
                + $"{Environment.NewLine}jnz 1 -21";

            Assert.Equal(158, task.Solution(instructions));
        }
    }
}
