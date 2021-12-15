using System;
using App.Tasks.Year2021.Day15;
using Xunit;

namespace Tests.Tasks.Year2021.Day15
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_RiskLevelMapExample_LowestTotalRiskOfAnyPathFromTopLeftToBottomRightEquals()
        {
            string riskLevelMap = "1163751742"
                + $"{Environment.NewLine}1381373672"
                + $"{Environment.NewLine}2136511328"
                + $"{Environment.NewLine}3694931569"
                + $"{Environment.NewLine}7463417111"
                + $"{Environment.NewLine}1319128137"
                + $"{Environment.NewLine}1359912421"
                + $"{Environment.NewLine}3125421639"
                + $"{Environment.NewLine}1293138521"
                + $"{Environment.NewLine}2311944581";


            Assert.Equal(40, task.Solution(riskLevelMap));
        }
    }
}
