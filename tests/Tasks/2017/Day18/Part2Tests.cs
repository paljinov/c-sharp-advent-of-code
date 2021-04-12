using System;
using App.Tasks.Year2017.Day18;
using Xunit;

namespace Tests.Tasks.Year2017.Day18
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_ProgramOneTotalSentValuesWhenAssemblyCodeIsRanTwiceEquals()
        {
            string instructions = "snd 1"
                + $"{Environment.NewLine}snd 2"
                + $"{Environment.NewLine}snd p"
                + $"{Environment.NewLine}rcv a"
                + $"{Environment.NewLine}rcv b"
                + $"{Environment.NewLine}rcv c"
                + $"{Environment.NewLine}rcv d";

            Assert.Equal(3, task.Solution(instructions));
        }
    }
}
