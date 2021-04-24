using System;
using App.Tasks.Year2018.Day3;
using Xunit;

namespace Tests.Tasks.Year2018.Day3
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ClaimsExample_SquareInchesOfFabricWithinTwoOrMoreClaimsEquals()
        {
            string claims = "#1 @ 1,3: 4x4"
                + $"{Environment.NewLine}#2 @ 3,1: 4x4"
                + $"{Environment.NewLine}#3 @ 5,5: 2x2";

            Assert.Equal(4, task.Solution(claims));
        }
    }
}
