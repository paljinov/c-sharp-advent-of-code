using App.Tasks.Year2015.Day6;
using Xunit;

namespace Tests.Tasks.Year2015.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("turn on 0,0 through 999,999", 1000000)]
        [InlineData("toggle 0,0 through 999,0", 1000)]
        [InlineData("turn off 499,499 through 500,500", 0)]
        public void Solution_InstructionsExample_LitLightsEquals(string instructions, int litLights)
        {
            Assert.Equal(litLights, task.Solution(instructions));
        }
    }
}
