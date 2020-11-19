using App.Tasks.Year2015.Day1;
using Xunit;

namespace Tests.Tasks.Year2015.Day1
{
    public class Part1Tests
    {
        [Fact]
        public void Solution_ExampleInstructions_FloorZero()
        {
            Part1 task = new Part1();
            int result = 0;

            Assert.Equal(result, task.Solution("(())"));
            Assert.Equal(result, task.Solution("()()"));
        }

        [Fact]
        public void Solution_ExampleInstructions_FloorThree()
        {
            Part1 task = new Part1();
            int result = 3;

            Assert.Equal(result, task.Solution("((("));
            Assert.Equal(result, task.Solution("(()(()("));
            Assert.Equal(result, task.Solution("))((((("));
        }

        [Fact]
        public void Solution_ExampleInstructions_FloorMinusOne()
        {
            Part1 task = new Part1();
            int result = -1;

            Assert.Equal(result, task.Solution("())"));
            Assert.Equal(result, task.Solution("))("));
        }

        [Fact]
        public void Solution_ExampleInstructions_FloorMinusThree()
        {
            Part1 task = new Part1();
            int result = -3;

            Assert.Equal(result, task.Solution(")))"));
            Assert.Equal(result, task.Solution(")())())"));
        }
    }
}
