using App.Tasks.Year2015.Day2;
using Xunit;

namespace Tests.Tasks.Year2015.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("2x3x4", 58)]
        [InlineData("1x1x10", 43)]
        public void Solution_DimensionsExample_WrappingPaperSquareFeetEquals(
            string dimensions,
            int wrappingPaperSquareFeet
        )
        {
            Assert.Equal(wrappingPaperSquareFeet, task.Solution(dimensions));
        }
    }
}
