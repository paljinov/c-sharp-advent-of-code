using App.Tasks.Year2018.Day9;
using Xunit;

namespace Tests.Tasks.Year2018.Day9
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("9 players; last marble is worth 25 points", 22563)]
        [InlineData("10 players; last marble is worth 1618 points", 74765078)]
        [InlineData("13 players; last marble is worth 7999 points", 1406506154)]
        [InlineData("17 players; last marble is worth 1104 points", 20548882)]
        [InlineData("21 players; last marble is worth 6111 points", 507583214)]
        [InlineData("30 players; last marble is worth 5807 points", 320997431)]
        public void Solution_MarblesGameExample_WinningElfScoreEquals(string marblesGame, int winningElfScore)
        {
            Assert.Equal(winningElfScore, task.Solution(marblesGame));
        }
    }
}
