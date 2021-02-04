using App.Tasks.Year2017.Day3;
using Xunit;

namespace Tests.Tasks.Year2017.Day3
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("4", 5)]
        [InlineData("11", 23)]
        [InlineData("26", 54)]
        [InlineData("304", 330)]
        [InlineData("747", 806)]
        public void Solution_InputSquareExample_FirstValueLargerThanInputSquareEquals(
            string inputSquare,
            int firstValueLargerThanInputSquare
        )
        {
            Assert.Equal(firstValueLargerThanInputSquare, task.Solution(inputSquare));
        }
    }
}
