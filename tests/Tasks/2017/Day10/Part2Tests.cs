using App.Tasks.Year2017.Day10;
using Xunit;

namespace Tests.Tasks.Year2017.Day10
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_LengthsSequenceExample_KnotHashEquals()
        {
            string lengthsSequence = "3, 4, 1, 5";
            Assert.Equal("00423f1d90f800d9c7a913fd7cd6df24", task.Solution(lengthsSequence));
        }
    }
}
