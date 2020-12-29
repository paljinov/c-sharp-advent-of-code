using App.Tasks.Year2020.Day23;
using Xunit;

namespace Tests.Tasks.Year2020.Day23
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void SolutionCupsLabelsExample_ProductOfTwoCupsLabelsClockwiseFromOneEquals()
        {
            Assert.Equal(149245887792, task.Solution("389125467"));
        }
    }
}
