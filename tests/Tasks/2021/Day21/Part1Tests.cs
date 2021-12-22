using System;
using App.Tasks.Year2021.Day21;
using Xunit;

namespace Tests.Tasks.Year2021.Day21
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_PlayersStartingPositionsExample_ProductOfLosingPlayerScoreMultipliedByNumberOfDieRollsEquals()
        {
            string playersStartingPositions = "Player 1 starting position: 4"
                + $"{Environment.NewLine}Player 2 starting position: 8";

            Assert.Equal(45, task.Solution(playersStartingPositions));
        }
    }
}
