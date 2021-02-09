using System;
using App.Tasks.Year2016.Day2;
using Xunit;

namespace Tests.Tasks.Year2016.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_BathroomCodeEquals()
        {
            string instructions = "ULL"
                + $"{Environment.NewLine}RRDDD"
                + $"{Environment.NewLine}LURDL"
                + $"{Environment.NewLine}UUUUD";

            Assert.Equal("1985", task.Solution(instructions));
        }
    }
}
