using App.Tasks.Year2015.Day3;
using Xunit;

namespace Tests.Tasks.Year2015.Day3
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData(">")]
        [InlineData("^v^v^v^v^v")]
        public void Solution_ExampleDirections_DeliversPresentsToTwoHouses(string directions)
        {
            Assert.Equal(2, task.Solution(directions));
        }

        [Theory]
        [InlineData("^>v<")]
        public void Solution_ExampleDirections_DeliversPresentsToFourHouses(string directions)
        {
            Assert.Equal(4, task.Solution(directions));
        }
    }
}
