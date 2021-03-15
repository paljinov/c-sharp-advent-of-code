using App.Tasks.Year2017.Day11;
using Xunit;

namespace Tests.Tasks.Year2017.Day11
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 0)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Solution_PathsDirectionsExample_FewestNumberOfStepsToReachChildProcessEquals(
            string pathsDirections,
            int fewestNumberOfStepsToReachChildProcess
        )
        {
            Assert.Equal(fewestNumberOfStepsToReachChildProcess, task.Solution(pathsDirections));
        }
    }
}
