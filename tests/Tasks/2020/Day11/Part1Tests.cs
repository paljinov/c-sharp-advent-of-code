using System;
using App.Tasks.Year2020.Day11;
using Xunit;

namespace Tests.Tasks.Year2020.Day11
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_SeatsLayoutExample_FinallyOccupiedSeatsForAdjacentSeatsDecisionEquals()
        {
            string seats = "L.LL.LL.LL"
                + $"{Environment.NewLine}LLLLLLL.LL"
                + $"{Environment.NewLine}L.L.L..L.."
                + $"{Environment.NewLine}LLLL.LL.LL"
                + $"{Environment.NewLine}L.LL.LL.LL"
                + $"{Environment.NewLine}L.LLLLL.LL"
                + $"{Environment.NewLine}..L.L....."
                + $"{Environment.NewLine}LLLLLLLLLL"
                + $"{Environment.NewLine}L.LLLLLL.L"
                + $"{Environment.NewLine}L.LLLLL.LL";

            Assert.Equal(37, task.Solution(seats));
        }
    }
}
