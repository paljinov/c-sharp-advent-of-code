using System;
using App.Tasks.Year2017.Day5;
using Xunit;

namespace Tests.Tasks.Year2017.Day5
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_JumpOffsetsExample_StepsToReachExitCountEquals()
        {
            string jumpOffsets = "0"
                + $"{Environment.NewLine}3"
                + $"{Environment.NewLine}0"
                + $"{Environment.NewLine}1"
                + $"{Environment.NewLine}-3";

            Assert.Equal(10, task.Solution(jumpOffsets));
        }
    }
}
