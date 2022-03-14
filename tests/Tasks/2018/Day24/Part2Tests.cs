using System;
using App.Tasks.Year2018.Day24;
using Xunit;

namespace Tests.Tasks.Year2018.Day24
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ReindeerConditionExample_ImmuneSystemUnitsCountWhichAreLeftAfterGettingTheSmallestBoostNeededToWinEquals()
        {
            string reindeerCondition = "Immune System:"
                + $"{Environment.NewLine}17 units each with 5390 hit points (weak to radiation, bludgeoning) with"
                + " an attack that does 4507 fire damage at initiative 2"
                + $"{Environment.NewLine}989 units each with 1274 hit points (immune to fire; weak to bludgeoning,"
                + " slashing) with an attack that does 25 slashing damage at initiative 3"
                + Environment.NewLine
                + $"{Environment.NewLine}Infection:"
                + $"{Environment.NewLine}801 units each with 4706 hit points (weak to radiation) with an attack"
                + " that does 116 bludgeoning damage at initiative 1"
                + $"{Environment.NewLine}4485 units each with 2961 hit points (immune to radiation; weak to fire,"
                + " cold) with an attack that does 12 slashing damage at initiative 4";

            Assert.Equal(51, task.Solution(reindeerCondition));
        }
    }
}
