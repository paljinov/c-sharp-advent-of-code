using App.Tasks.Year2020.Day15;
using Xunit;

namespace Tests.Tasks.Year2020.Day15
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("0,3,6", 436)]
        [InlineData("1,3,2", 1)]
        [InlineData("2,1,3", 10)]
        [InlineData("1,2,3", 27)]
        [InlineData("2,3,1", 78)]
        [InlineData("3,2,1", 438)]
        [InlineData("3,1,2", 1836)]
        public void Solution_StartingNumbersExample_2020thSpokenNumberEquals(
            string startingNumbers,
            int spokenNumberAtPosition2020
        )
        {
            Assert.Equal(spokenNumberAtPosition2020, task.Solution(startingNumbers));
        }
    }
}
