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
        [InlineData("^v", 3)]
        [InlineData("^>v<", 3)]
        [InlineData("^v^v^v^v^v", 11)]
        public void Solution_ExampleInstructions_HousesThatReceiveAtLeastOnePresentWithRoboSantaCountEquals(
            string instructions,
            int housesThatReceiveAtLeastOnePresentWithRoboSanta
        )
        {
            Assert.Equal(housesThatReceiveAtLeastOnePresentWithRoboSanta, task.Solution(instructions));
        }
    }
}
