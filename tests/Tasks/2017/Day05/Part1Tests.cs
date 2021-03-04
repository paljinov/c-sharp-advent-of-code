using System;
using App.Tasks.Year2017.Day5;
using Xunit;

namespace Tests.Tasks.Year2017.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_JumpOffsetsExample_StepsToReachExitCountEquals()
        {
            string jumpOffsets = "0"
                + $"{Environment.NewLine}3"
                + $"{Environment.NewLine}0"
                + $"{Environment.NewLine}1"
                + $"{Environment.NewLine}-3";

            Assert.Equal(5, task.Solution(jumpOffsets));
        }
    }
}
