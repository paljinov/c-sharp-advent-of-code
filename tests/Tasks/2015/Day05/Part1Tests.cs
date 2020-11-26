using App.Tasks.Year2015.Day5;
using Xunit;

namespace Tests.Tasks.Year2015.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("ugknbfddgicrmopn")]
        [InlineData("aaa")]
        public void Solution_ExampleString_Nice(string @string)
        {
            Assert.Equal(1, task.Solution(@string));
        }

        [Theory]
        [InlineData("jchzalrnumimnmhp")]
        [InlineData("haegwjzuvuyypxyu")]
        [InlineData("dvszwmarrgswjxmb")]
        public void Solution_ExampleString_Naughty(string @string)
        {
            Assert.Equal(0, task.Solution(@string));
        }
    }
}
