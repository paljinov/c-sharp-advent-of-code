using System;
using App.Tasks.Year2022.Day2;
using Xunit;

namespace Tests.Tasks.Year2022.Day2
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_StrategyGuideExample_TotalScoreWhenSecondColumnSaysHowRoundNeedsToEndEquals()
        {
            string strategyGuide = "A Y"
                + $"{Environment.NewLine}B X"
                + $"{Environment.NewLine}C Z";

            Assert.Equal(12, task.Solution(strategyGuide));
        }
    }
}
