using App.Tasks.Year2015.Day2;
using Xunit;

namespace Tests.Tasks.Year2015.Day2
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("2x3x4", 34)]
        [InlineData("1x1x10", 14)]
        public void Solution_DimensionsExample_FeetOfRibbonEquals(string dimensions, int wrappingPaperSquareFeet)
        {
            Assert.Equal(wrappingPaperSquareFeet, task.Solution(dimensions));
        }
    }
}
