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

        [Fact]
        public void Solution_FirstExampleInstructions_LitLightsEquals()
        {
            Assert.Equal(1000000, task.Solution("turn on 0,0 through 999,999"));
        }

        [Fact]
        public void Solution_SecondExampleInstructions_LitLightsEquals()
        {
            Assert.Equal(1000, task.Solution("toggle 0,0 through 999,0"));
        }

        [Fact]
        public void Solution_ThirdExampleInstructions_LitLightsEquals()
        {
            Assert.Equal(0, task.Solution("turn off 499,499 through 500,500"));
        }
    }
}
