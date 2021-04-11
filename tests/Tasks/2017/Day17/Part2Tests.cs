using App.Tasks.Year2017.Day17;
using Xunit;

namespace Tests.Tasks.Year2017.Day17
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_StepsExample_ValueAfterZeroWhenFiftyMillionIsInsertedEquals()
        {
            string danceMoves = "3";
            Assert.Equal(1222153, task.Solution(danceMoves));
        }
    }
}
