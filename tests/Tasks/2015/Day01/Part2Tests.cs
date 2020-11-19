using App.Tasks.Year2015.Day1;
using Xunit;

namespace Tests.Tasks.Year2015.Day1
{
    public class Part2Tests
    {
        [Fact]
        public void Solution_ExampleInstructions_BasementAtCharacterPositionOne()
        {
            Part2 task = new Part2();
            int result = 1;

            Assert.Equal(result, task.Solution(")"));
        }

        [Fact]
        public void Solution_ExampleInstructions_BasementAtCharacterPositionFive()
        {
            Part2 task = new Part2();
            int result = 5;

            Assert.Equal(result, task.Solution("()())"));
        }
    }
}
