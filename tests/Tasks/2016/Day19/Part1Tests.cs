using App.Tasks.Year2016.Day19;
using Xunit;

namespace Tests.Tasks.Year2016.Day19
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_TotalElvesExample_ElfWhichGetsAllThePresentsEquals()
        {
            string totalElves = "5";
            Assert.Equal(3, task.Solution(totalElves));
        }
    }
}
