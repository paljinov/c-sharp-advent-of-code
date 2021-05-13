using App.Tasks.Year2018.Day14;
using Xunit;

namespace Tests.Tasks.Year2018.Day14
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("5", "0124515891")]
        [InlineData("9", "5158916779")]
        [InlineData("18", "9251071085")]
        [InlineData("2018", "5941429882")]
        public void Solution_NumberOfRecipesExample_ScoresOfTenRecipesImmediatelyAfterInputNumberOfRecipesEquals(
            string numberOfRecipes,
            string scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes
        )
        {
            Assert.Equal(scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes, task.Solution(numberOfRecipes));
        }
    }
}
