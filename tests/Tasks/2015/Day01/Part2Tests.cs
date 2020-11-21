using App.Tasks.Year2015.Day1;
using Xunit;

namespace Tests.Tasks.Year2015.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ExampleInstructions_BasementAtCharacterPositionOne()
        {
            Assert.Equal(1, task.Solution(")"));
        }

        [Fact]
        public void Solution_ExampleInstructions_BasementAtCharacterPositionFive()
        {
            Assert.Equal(5, task.Solution("()())"));
        }
    }
}
