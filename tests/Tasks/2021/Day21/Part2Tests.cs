using System;
using App.Tasks.Year2021.Day21;
using Xunit;

namespace Tests.Tasks.Year2021.Day21
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_PlayersStartingPositionsExample_NumberOfUniversesInWhichWinningPlayerWinsEquals()
        {
            string playersStartingPositions = "Player 1 starting position: 4"
                + $"{Environment.NewLine}Player 2 starting position: 8";

            Assert.Equal(444356092776315, task.Solution(playersStartingPositions));
        }
    }
}
