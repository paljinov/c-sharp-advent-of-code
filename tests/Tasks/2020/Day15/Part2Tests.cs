using App.Tasks.Year2020.Day15;
using Xunit;

namespace Tests.Tasks.Year2020.Day15
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("0,3,6", 175594)]
        [InlineData("1,3,2", 2578)]
        [InlineData("2,1,3", 3544142)]
        [InlineData("1,2,3", 261214)]
        [InlineData("2,3,1", 6895259)]
        [InlineData("3,2,1", 18)]
        [InlineData("3,1,2", 362)]
        public void Solution_StartingNumbersExample_30000000thSpokenNumberEquals(
            string startingNumbers,
            int spokenNumberAtPosition30000000
        )
        {
            Assert.Equal(spokenNumberAtPosition30000000, task.Solution(startingNumbers));
        }
    }
}
