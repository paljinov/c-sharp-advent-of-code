using App.Tasks.Year2021.Day7;
using Xunit;

namespace Tests.Tasks.Year2021.Day7
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_CrabSubmarinesHorizontalPositionsExample_LeastSpentFuelWhichIsNeededToAlignEquals()
        {
            string crabSubmarinesHorizontalPositions = "16,1,2,0,4,2,7,1,2,14";
            Assert.Equal(37, task.Solution(crabSubmarinesHorizontalPositions));
        }
    }
}
