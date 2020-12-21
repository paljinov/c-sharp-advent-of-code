using System;
using App.Tasks.Year2020.Day21;
using Xunit;

namespace Tests.Tasks.Year2020.Day21
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FoodsExample_AppearancesOfIngredientsWithoutAllergensEquals()
        {
            string foods = "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)"
                + $"{Environment.NewLine}trh fvjkl sbzzf mxmxvkd (contains dairy)"
                + $"{Environment.NewLine}sqjhc fvjkl (contains soy)"
                + $"{Environment.NewLine}sqjhc mxmxvkd sbzzf (contains fish)";

            Assert.Equal(5, task.Solution(foods));
        }
    }
}
