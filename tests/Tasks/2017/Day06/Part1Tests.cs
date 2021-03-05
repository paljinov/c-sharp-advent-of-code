using App.Tasks.Year2017.Day6;
using Xunit;

namespace Tests.Tasks.Year2017.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_MemoryBanksBlocksExample_RedistributionCyclesCountEquals()
        {
            string memoryBanksBlocks = "0	2	7	0";

            Assert.Equal(5, task.Solution(memoryBanksBlocks));
        }
    }
}
