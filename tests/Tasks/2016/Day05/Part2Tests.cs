using App.Tasks.Year2016.Day5;
using Xunit;

namespace Tests.Tasks.Year2016.Day5
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_DoorIdExample_PasswordForPositionConditionEquals()
        {
            string doorId = "abc";
            Assert.Equal("05ace8e3", task.Solution(doorId));
        }
    }
}
