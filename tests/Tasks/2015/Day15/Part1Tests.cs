using System;
using App.Tasks.Year2015.Day15;
using Xunit;

namespace Tests.Tasks.Year2015.Day15
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_IngredientsExample_HighestScoringCookieEquals()
        {
            string ingredients = "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8"
                + $"{Environment.NewLine}Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";

            Assert.Equal(62842880, task.Solution(ingredients));
        }
    }
}
