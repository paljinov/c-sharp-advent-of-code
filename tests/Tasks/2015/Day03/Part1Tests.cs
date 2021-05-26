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
        [InlineData(">", 2)]
        [InlineData("^>v<", 4)]
        [InlineData("^v^v^v^v^v", 2)]
        public void Solution_ExampleInstructions_HousesThatReceiveAtLeastOnePresentCountEquals(
            string instructions,
            int housesThatReceiveAtLeastOnePresent
        )
        {
            Assert.Equal(housesThatReceiveAtLeastOnePresent, task.Solution(instructions));
        }
    }
}
