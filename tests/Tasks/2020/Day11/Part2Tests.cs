using System;
using App.Tasks.Year2020.Day11;
using Xunit;

namespace Tests.Tasks.Year2020.Day11
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_SeatsLayoutExample_FinallyOccupiedSeatsForFirstSeenSeatsDecisionEquals()
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

            Assert.Equal(26, task.Solution(seats));
        }
    }
}
