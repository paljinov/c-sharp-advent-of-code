using App.Tasks.Year2018.Day11;
using Xunit;

namespace Tests.Tasks.Year2018.Day11
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("18", "90,269,16")]
        [InlineData("42", "232,251,12")]
        public void Solution_GridSerialNumberExample_IdentifierOfTheSquareWithTheLargestTotalPowerEquals(
            string gridSerialNumber,
            string squareIdentifier
        )
        {
            Assert.Equal(squareIdentifier, task.Solution(gridSerialNumber));
        }
    }
}
