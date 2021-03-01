using App.Tasks.Year2016.Day5;
using Xunit;

namespace Tests.Tasks.Year2016.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_DoorIdExample_PasswordEquals()
        {
            string doorId = "abc";
            Assert.Equal("18f47a30", task.Solution(doorId));
        }
    }
}
