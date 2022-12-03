using System;
using App.Tasks.Year2022.Day1;
using Xunit;

namespace Tests.Tasks.Year2022.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_CaloriesPerElfExample_TotalCaloriesForTopThreeElvesCarryingTheMostEquals()
        {
            string totalCaloriesForTopThreeElvesCarryingTheMost = "1000"
                + $"{Environment.NewLine}2000"
                + $"{Environment.NewLine}3000"
                + Environment.NewLine
                + $"{Environment.NewLine}4000"
                + Environment.NewLine
                + $"{Environment.NewLine}5000"
                + $"{Environment.NewLine}6000"
                + Environment.NewLine
                + $"{Environment.NewLine}7000"
                + $"{Environment.NewLine}8000"
                + $"{Environment.NewLine}9000"
                + Environment.NewLine
                + $"{Environment.NewLine}10000";

            Assert.Equal(45000, task.Solution(totalCaloriesForTopThreeElvesCarryingTheMost));
        }
    }
}
