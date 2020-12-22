using System;
using App.Tasks.Year2020.Day22;
using Xunit;

namespace Tests.Tasks.Year2020.Day22
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_PlayersStartingDecksExample_RecursiveCombatWinningPlayerScoreEquals()
        {
            string playersStartingDecks = "Player 1:"
                + $"{Environment.NewLine}9"
                + $"{Environment.NewLine}2"
                + $"{Environment.NewLine}6"
                + $"{Environment.NewLine}3"
                + $"{Environment.NewLine}1"
                + Environment.NewLine
                + $"{Environment.NewLine}Player 2:"
                + $"{Environment.NewLine}5"
                + $"{Environment.NewLine}8"
                + $"{Environment.NewLine}4"
                + $"{Environment.NewLine}7"
                + $"{Environment.NewLine}10";

            Assert.Equal(291, task.Solution(playersStartingDecks));
        }
    }
}
