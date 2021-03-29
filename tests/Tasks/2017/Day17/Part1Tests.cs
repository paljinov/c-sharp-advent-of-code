using App.Tasks.Year2017.Day17;
using Xunit;

namespace Tests.Tasks.Year2017.Day17
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_StepsExample_ValueAfterAndWhenTwoThousandSeventeenValueIsInsertedEquals()
        {
            string danceMoves = "3";
            Assert.Equal(638, task.Solution(danceMoves));
        }
    }
}
