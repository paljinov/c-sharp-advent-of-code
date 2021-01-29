using System;
using System.Reflection;
using App.Tasks.Year2015.Day21;
using Xunit;

namespace Tests.Tasks.Year2015.Day21
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            Fight fight = new Fight();
            fight.GetType()
                .GetField("playerHitPoints", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(fight, 8);

            task.GetType()
                .GetField("fight", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, fight);
        }

        [Fact]
        public void Solution_BossStatsExample_LeastAmountOfGoldSpentWhenWinningEquals()
        {
            string bossStats = "Hit Points: 12"
                + $"{Environment.NewLine}Damage: 7"
                + $"{Environment.NewLine}Armor: 2";

            Assert.Equal(188, task.Solution(bossStats));
        }
    }
}
