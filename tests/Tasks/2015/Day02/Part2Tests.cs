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

        [Fact]
        public void Solution_FirstExampleDimensions_FeetOfRibbonCorrectCalculation()
        {
            Assert.Equal(34, task.Solution("2x3x4"));
        }

        [Fact]
        public void Solution_SecondExampleDimensions_FeetOfRibbonCorrectCalculation()
        {
            Assert.Equal(14, task.Solution("1x1x10"));
        }
    }
}
