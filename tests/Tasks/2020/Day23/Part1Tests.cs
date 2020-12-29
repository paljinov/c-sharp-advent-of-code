using App.Tasks.Year2020.Day23;
using Xunit;

namespace Tests.Tasks.Year2020.Day23
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void SolutionCupsLabelsExample_LabelsOnCupsAfterOneHundredMovesClockwiseFromOneEquals()
        {
            Assert.Equal("67384529", task.Solution("389125467"));
        }
    }
}
