using App.Tasks.Year2020.Day5;
using Xunit;

namespace Tests.Tasks.Year2020.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void Solution_BoardingPassExample_SeatIdEquals(string boardingPass, int seatId)
        {
            Assert.Equal(seatId, task.Solution(boardingPass));
        }
    }
}
