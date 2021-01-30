using App.Tasks.Year2016.Day1;
using Xunit;

namespace Tests.Tasks.Year2016.Day1
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("R2, L3", 5)]
        [InlineData("R2, R2, R2", 2)]
        [InlineData("R5, L5, R5, R3", 12)]
        public void Solution_InstructionsExample_BlocksAwayFromEasterBunnyHQEquals(
            string instructions,
            int blocksAway
        )
        {
            Assert.Equal(blocksAway, task.Solution(instructions));
        }
    }
}
