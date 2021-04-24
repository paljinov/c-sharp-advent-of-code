using System;
using App.Tasks.Year2018.Day3;
using Xunit;

namespace Tests.Tasks.Year2018.Day3
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ClaimsExample_IdOfTheOnlyClaimThatDoesntOverlapEquals()
        {
            string claims = "#1 @ 1,3: 4x4"
                + $"{Environment.NewLine}#2 @ 3,1: 4x4"
                + $"{Environment.NewLine}#3 @ 5,5: 2x2";

            Assert.Equal(3, task.Solution(claims));
        }
    }
}
