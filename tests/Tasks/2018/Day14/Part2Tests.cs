using App.Tasks.Year2018.Day14;
using Xunit;

namespace Tests.Tasks.Year2018.Day14
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("01245", 5)]
        [InlineData("51589", 9)]
        [InlineData("92510", 18)]
        [InlineData("59414", 2018)]
        public void Solution_RecipesScoreSequenceExample_RecipesWhichAppearToTheLeftOfTheScoreSequenceEquals(
            string recipesScoreSequence,
            int recipesWhichAppearToTheLeftOfTheScoreSequence
        )
        {
            Assert.Equal(recipesWhichAppearToTheLeftOfTheScoreSequence, task.Solution(recipesScoreSequence));
        }
    }
}
