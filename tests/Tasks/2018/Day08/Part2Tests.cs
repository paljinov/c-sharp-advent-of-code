using App.Tasks.Year2018.Day8;
using Xunit;

namespace Tests.Tasks.Year2018.Day8
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_NumbersExample_RootNodeValueEquals()
        {
            string numbers = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
            Assert.Equal(66, task.Solution(numbers));
        }
    }
}
