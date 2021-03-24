using App.Tasks.Year2017.Day14;
using Xunit;

namespace Tests.Tasks.Year2017.Day14
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_KeyExample_RegionsCountEquals()
        {
            string key = "flqrgnkx";
            Assert.Equal(1242, task.Solution(key));
        }
    }
}
