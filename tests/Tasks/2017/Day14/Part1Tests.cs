using App.Tasks.Year2017.Day14;
using Xunit;

namespace Tests.Tasks.Year2017.Day14
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_KeyExample_UsedSquaresEquals()
        {
            string key = "flqrgnkx";
            Assert.Equal(8108, task.Solution(key));
        }
    }
}
