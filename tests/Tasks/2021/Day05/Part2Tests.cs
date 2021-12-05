using System;
using App.Tasks.Year2021.Day5;
using Xunit;

namespace Tests.Tasks.Year2021.Day5
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_LineSegmentsExample_PointsWhereAtLeastTwoLinesOverlapWhenConsideringDiagonalLinesCountEquals()
        {
            string lineSegments = "0,9 -> 5,9"
                + $"{Environment.NewLine}8,0 -> 0,8"
                + $"{Environment.NewLine}9,4 -> 3,4"
                + $"{Environment.NewLine}2,2 -> 2,1"
                + $"{Environment.NewLine}7,0 -> 7,4"
                + $"{Environment.NewLine}6,4 -> 2,0"
                + $"{Environment.NewLine}0,9 -> 2,9"
                + $"{Environment.NewLine}3,4 -> 1,4"
                + $"{Environment.NewLine}0,0 -> 8,8"
                + $"{Environment.NewLine}5,5 -> 8,2";

            Assert.Equal(12, task.Solution(lineSegments));
        }
    }
}
