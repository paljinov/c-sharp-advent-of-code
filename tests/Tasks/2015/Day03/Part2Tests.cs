using App.Tasks.Year2015.Day3;
using Xunit;

namespace Tests.Tasks.Year2015.Day3
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("^v")]
        [InlineData("^>v<")]
        public void Solution_ExampleDirections_ThreeHousesWhichReceiveAtLeastOnePresent(string directions)
        {
            Assert.Equal(3, task.Solution(directions));
        }

        [Theory]
        [InlineData("^v^v^v^v^v")]
        public void Solution_ExampleDirections_ElevenHousesWhichReceiveAtLeastOnePresent(string directions)
        {
            Assert.Equal(11, task.Solution(directions));
        }
    }
}
