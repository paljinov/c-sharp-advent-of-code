using App.Tasks.Year2016.Day1;
using Xunit;

namespace Tests.Tasks.Year2016.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InstructionsExample_BlocksAwayForFirstLocationWhichIsVisitedTwice()
        {
            Assert.Equal(4, task.Solution("R8, R4, R4, R8"));
        }
    }
}
