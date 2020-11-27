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


        [Fact]
        public void Solution_FirstExampleInstructionsTotalBrightnessIncreaseEquals()
        {
            Assert.Equal(1, task.Solution("turn on 0,0 through 0,0"));
        }

        [Fact]
        public void Solution_SecondExampleInstructions_TotalBrightnessIncreaseEquals()
        {
            Assert.Equal(2000000, task.Solution("toggle 0,0 through 999,999"));
        }
    }
}
