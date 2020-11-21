using App.Tasks.Year2015.Day1;
using Xunit;

namespace Tests.Tasks.Year2015.Day1
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("(())")]
        [InlineData("()()")]
        public void Solution_ExampleInstructions_FloorZero(string parentheses)
        {
            Assert.Equal(0, task.Solution(parentheses));
        }

        [Theory]
        [InlineData("(((")]
        [InlineData("(()(()(")]
        [InlineData("))(((((")]
        public void Solution_ExampleInstructions_FloorThree(string parentheses)
        {
            Assert.Equal(3, task.Solution(parentheses));
        }

        [Theory]
        [InlineData("())")]
        [InlineData("))(")]
        public void Solution_ExampleInstructions_FloorMinusOne(string parentheses)
        {
            Assert.Equal(-1, task.Solution(parentheses));
        }

        [Theory]
        [InlineData(")))")]
        [InlineData(")())())")]
        public void Solution_ExampleInstructions_FloorMinusThree(string parentheses)
        {
            Assert.Equal(-3, task.Solution(parentheses));
        }
    }
}
