using System;
using App.Tasks.Year2022.Day2;
using Xunit;

namespace Tests.Tasks.Year2022.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_StrategyGuideExample_TotalScoreEquals()
        {
            string strategyGuide = "A Y"
                + $"{Environment.NewLine}B X"
                + $"{Environment.NewLine}C Z";

            Assert.Equal(15, task.Solution(strategyGuide));
        }
    }
}
