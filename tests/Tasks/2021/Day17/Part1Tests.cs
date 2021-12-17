using App.Tasks.Year2021.Day17;
using Xunit;

namespace Tests.Tasks.Year2021.Day17
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_TargetAreaExample_HighestPositionProbeReachesOnThisTrajectoryEquals()
        {
            string targetArea = "target area: x=20..30, y=-10..-5";
            Assert.Equal(45, task.Solution(targetArea));
        }
    }
}
