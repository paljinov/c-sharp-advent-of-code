using System;
using App.Tasks.Year2021.Day13;
using Xunit;

namespace Tests.Tasks.Year2021.Day13
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_DotsAndFoldInstructionsExample_DotsCountAfterCompletingFirstFoldInstructionEquals()
        {
            string dotsAndFoldInstructions = "6,10"
                + $"{Environment.NewLine}0,14"
                + $"{Environment.NewLine}9,10"
                + $"{Environment.NewLine}0,3"
                + $"{Environment.NewLine}10,4"
                + $"{Environment.NewLine}4,11"
                + $"{Environment.NewLine}6,0"
                + $"{Environment.NewLine}6,12"
                + $"{Environment.NewLine}4,1"
                + $"{Environment.NewLine}0,13"
                + $"{Environment.NewLine}10,12"
                + $"{Environment.NewLine}3,4"
                + $"{Environment.NewLine}3,0"
                + $"{Environment.NewLine}8,4"
                + $"{Environment.NewLine}1,10"
                + $"{Environment.NewLine}2,14"
                + $"{Environment.NewLine}8,10"
                + $"{Environment.NewLine}9,0"
                + Environment.NewLine
                + $"{Environment.NewLine}fold along y=7"
                + $"{Environment.NewLine}fold along x=5";

            Assert.Equal(17, task.Solution(dotsAndFoldInstructions));
        }
    }
}
