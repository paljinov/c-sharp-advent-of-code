using App.Tasks.Year2017.Day6;
using Xunit;

namespace Tests.Tasks.Year2017.Day6
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_MemoryBanksBlocksExample_LoopSizeCountEquals()
        {
            string memoryBanksBlocks = "0	2	7	0";

            Assert.Equal(4, task.Solution(memoryBanksBlocks));
        }
    }
}
