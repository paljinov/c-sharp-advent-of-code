using App.Tasks.Year2018.Day11;
using Xunit;

namespace Tests.Tasks.Year2018.Day11
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("18", "33,45")]
        [InlineData("42", "21,61")]
        public void Solution_GridSerialNumberExample_TopLeftFuelCellCoordinateOfThe3x3SquareWithLargestTotalPowerEquals(
            string gridSerialNumber,
            string coordinates
        )
        {
            Assert.Equal(coordinates, task.Solution(gridSerialNumber));
        }
    }
}
