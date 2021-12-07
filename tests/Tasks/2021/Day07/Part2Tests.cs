using App.Tasks.Year2021.Day7;
using Xunit;

namespace Tests.Tasks.Year2021.Day7
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_CrabSubmarinesHorizontalPositionsExample_LeastSpentFuelWhichIsNeededToAlignForIncreasingFuelCostEquals()
        {
            string crabSubmarinesHorizontalPositions = "16,1,2,0,4,2,7,1,2,14";
            Assert.Equal(168, task.Solution(crabSubmarinesHorizontalPositions));
        }
    }
}
