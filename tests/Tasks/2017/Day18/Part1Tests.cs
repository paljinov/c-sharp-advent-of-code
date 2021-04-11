using System;
using App.Tasks.Year2017.Day18;
using Xunit;

namespace Tests.Tasks.Year2017.Day18
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_FirstRecoveredFrequencyEquals()
        {
            string instructions = "set a 1"
                + $"{Environment.NewLine}add a 2"
                + $"{Environment.NewLine}mul a a"
                + $"{Environment.NewLine}mod a 5"
                + $"{Environment.NewLine}snd a"
                + $"{Environment.NewLine}set a 0"
                + $"{Environment.NewLine}rcv a"
                + $"{Environment.NewLine}jgz a -1"
                + $"{Environment.NewLine}set a 1"
                + $"{Environment.NewLine}jgz a -2";

            Assert.Equal(4, task.Solution(instructions));
        }
    }
}
