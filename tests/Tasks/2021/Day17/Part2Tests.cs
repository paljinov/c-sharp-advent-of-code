using App.Tasks.Year2021.Day17;
using Xunit;

namespace Tests.Tasks.Year2021.Day17
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_TargetAreaExample_DistinctInitialVelocitiesCountWhichCauseProbeToBeWithinTargetAreaEquals()
        {
            string targetArea = "target area: x=20..30, y=-10..-5";
            Assert.Equal(112, task.Solution(targetArea));
        }
    }
}
