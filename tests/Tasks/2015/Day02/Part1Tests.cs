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

        [Fact]
        public void Solution_FirstExampleDimensions_WrappingPaperSquareFeetCorrectCalculation()
        {
            Assert.Equal(58, task.Solution("2x3x4"));
        }

        [Fact]
        public void Solution_SecondExampleDimensions_WrappingPaperSquareFeetCorrectCalculation()
        {
            Assert.Equal(43, task.Solution("1x1x10"));
        }
    }
}
