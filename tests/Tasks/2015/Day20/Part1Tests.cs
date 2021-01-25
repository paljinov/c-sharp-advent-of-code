using App.Tasks.Year2015.Day20;
using Xunit;

namespace Tests.Tasks.Year2015.Day20
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InputNumberExample_LowestHouseNumberToGetAtLeastAsManyPresentsAsInputNumberEquals()
        {
            Assert.Equal(6, task.Solution("120"));
        }
    }
}
