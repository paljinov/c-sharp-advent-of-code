using System;
using App.Tasks.Year2017.Day23;
using Xunit;

namespace Tests.Tasks.Year2017.Day23
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_RegisterHValueAfterProgramCompletionEquals()
        {
            string instructions = "set b 84"
                + $"{Environment.NewLine}set c b"
                + $"{Environment.NewLine}jnz a 2"
                + $"{Environment.NewLine}jnz 1 5"
                + $"{Environment.NewLine}mul b 100"
                + $"{Environment.NewLine}sub b -100000"
                + $"{Environment.NewLine}set c b"
                + $"{Environment.NewLine}sub c -17000"
                + $"{Environment.NewLine}set f 1"
                + $"{Environment.NewLine}set d 2"
                + $"{Environment.NewLine}set e 2"
                + $"{Environment.NewLine}set g d"
                + $"{Environment.NewLine}mul g e"
                + $"{Environment.NewLine}sub g b"
                + $"{Environment.NewLine}jnz g 2"
                + $"{Environment.NewLine}set f 0"
                + $"{Environment.NewLine}sub e -1"
                + $"{Environment.NewLine}set g e"
                + $"{Environment.NewLine}sub g b"
                + $"{Environment.NewLine}jnz g -8"
                + $"{Environment.NewLine}sub d -1"
                + $"{Environment.NewLine}set g d"
                + $"{Environment.NewLine}sub g b"
                + $"{Environment.NewLine}jnz g -13"
                + $"{Environment.NewLine}jnz f 2"
                + $"{Environment.NewLine}sub h -1"
                + $"{Environment.NewLine}set g b"
                + $"{Environment.NewLine}sub g c"
                + $"{Environment.NewLine}jnz g 2"
                + $"{Environment.NewLine}jnz 1 3"
                + $"{Environment.NewLine}sub b -17"
                + $"{Environment.NewLine}jnz 1 -23";

            Assert.Equal(903, task.Solution(instructions));
        }
    }
}
