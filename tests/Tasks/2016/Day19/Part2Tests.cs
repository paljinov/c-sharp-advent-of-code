using App.Tasks.Year2016.Day19;
using Xunit;

namespace Tests.Tasks.Year2016.Day19
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_TotalElvesExample_ElfWhichGetsAllThePresentsWhenStealingFromTheElfDirectlyAcrossTheCircleEquals()
        {
            string totalElves = "5";
            Assert.Equal(2, task.Solution(totalElves));
        }
    }
}
