using System;
using App.Tasks.Year2020.Day21;
using Xunit;

namespace Tests.Tasks.Year2020.Day21
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FoodsExample_CommaSeparatedDangerousIngredientsSortedByAllergenEquals()
        {
            string foods = "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)"
                + $"{Environment.NewLine}trh fvjkl sbzzf mxmxvkd (contains dairy)"
                + $"{Environment.NewLine}sqjhc fvjkl (contains soy)"
                + $"{Environment.NewLine}sqjhc mxmxvkd sbzzf (contains fish)";

            Assert.Equal("mxmxvkd,sqjhc,fvjkl", task.Solution(foods));
        }
    }
}
