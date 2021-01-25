using App.Tasks.Year2015.Day20;
using Xunit;

namespace Tests.Tasks.Year2015.Day20
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InputNumberExample_LowestHouseNumberToGetAtLeastAsManyPresentsAsInputNumberEquals()
        {
            Assert.Equal(6, task.Solution("130"));
        }
    }
}
