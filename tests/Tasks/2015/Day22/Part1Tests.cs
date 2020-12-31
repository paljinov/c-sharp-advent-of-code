using System;
using System.Reflection;
using App.Tasks.Year2015.Day22;
using Xunit;

namespace Tests.Tasks.Year2015.Day22
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            WizardSimulatorGame wizardSimulatorGame = new WizardSimulatorGame();
            // Preamble of 5 numbers
            wizardSimulatorGame.GetType()
                .GetField("playerHitPoints", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(wizardSimulatorGame, 10);

            wizardSimulatorGame.GetType()
                .GetField("playerManaPoints", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(wizardSimulatorGame, 250);

            task.GetType()
                .GetField("wizardSimulatorGame", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, wizardSimulatorGame);
        }

        [Fact]
        public void Solution_FirstBossStatsExample_LeastAmountOfManaSpentWhenWinningEquals()
        {
            string bossStats = "Hit Points: 13"
                + $"{Environment.NewLine}Damage: 8";

            Assert.Equal(226, task.Solution(bossStats));
        }

        [Fact]
        public void Solution_SecondBossStatsExample_LeastAmountOfManaSpentWhenWinningEquals()
        {
            string bossStats = "Hit Points: 14"
                + $"{Environment.NewLine}Damage: 8";

            Assert.Equal(641, task.Solution(bossStats));
        }
    }
}
