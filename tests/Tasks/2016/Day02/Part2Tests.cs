using System;
using App.Tasks.Year2016.Day2;
using Xunit;

namespace Tests.Tasks.Year2016.Day2
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_BathroomCodeEquals()
        {
            string instructions = "ULL"
                + $"{Environment.NewLine}RRDDD"
                + $"{Environment.NewLine}LURDL"
                + $"{Environment.NewLine}UUUUD";

            Assert.Equal("5DB3", task.Solution(instructions));
        }
    }
}
