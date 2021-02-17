using System;
using App.Tasks.Year2016.Day15;
using Xunit;

namespace Tests.Tasks.Year2016.Day15
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_DiscsExample_FirstTimeYouCanPressButtonAndGetCapsuleEquals()
        {
            string discs = "Disc #1 has 5 positions; at time=0, it is at position 4."
                + $"{Environment.NewLine}Disc #2 has 2 positions; at time=0, it is at position 1.";

            Assert.Equal(85, task.Solution(discs));
        }
    }
}
