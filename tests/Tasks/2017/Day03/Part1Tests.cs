using App.Tasks.Year2017.Day3;
using Xunit;

namespace Tests.Tasks.Year2017.Day3
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("1", 0)]
        [InlineData("12", 3)]
        [InlineData("23", 2)]
        [InlineData("1024", 31)]
        public void Solution_InputSquareExample_ManhattanDistanceFromSquareToAccessPortEquals(
            string inputSquare,
            int manhattanDistance
        )
        {
            Assert.Equal(manhattanDistance, task.Solution(inputSquare));
        }
    }
}
