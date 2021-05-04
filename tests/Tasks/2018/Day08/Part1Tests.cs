using App.Tasks.Year2018.Day8;
using Xunit;

namespace Tests.Tasks.Year2018.Day8
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_NumbersExample_SumOfAllMetadataEntriesEquals()
        {
            string numbers = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
            Assert.Equal(138, task.Solution(numbers));
        }
    }
}
