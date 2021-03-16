using App.Tasks.Year2017.Day11;
using Xunit;

namespace Tests.Tasks.Year2017.Day11
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 2)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Solution_PathsDirectionsExample_FurthestStepsEverEquals(
            string pathsDirections,
            int furthestStepsEver
        )
        {
            Assert.Equal(furthestStepsEver, task.Solution(pathsDirections));
        }
    }
}
