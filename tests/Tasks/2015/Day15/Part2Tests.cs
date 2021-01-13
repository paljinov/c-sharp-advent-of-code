using System;
using App.Tasks.Year2015.Day15;
using Xunit;

namespace Tests.Tasks.Year2015.Day15
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_IngredientsExample_HighestScoringCookieWithCalorieTotalOfFiveHundredEquals()
        {
            string ingredients = "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8"
                + $"{Environment.NewLine}Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";

            Assert.Equal(57600000, task.Solution(ingredients));
        }
    }
}
