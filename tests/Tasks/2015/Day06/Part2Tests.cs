using App.Tasks.Year2015.Day6;
using Xunit;

namespace Tests.Tasks.Year2015.Day6
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("turn on 0,0 through 0,0", 1)]
        [InlineData("toggle 0,0 through 999,999", 2000000)]
        public void Solution_InstructionsExample_TotalBrightnessIncreaseEquals(
            string instructions,
            int totalBrightnessIncrease
        )
        {
            Assert.Equal(totalBrightnessIncrease, task.Solution(instructions));
        }
    }
}
