using App.Tasks.Year2015.Day5;
using Xunit;

namespace Tests.Tasks.Year2015.Day5
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("qjhvhtzxzqqjkmpb")]
        [InlineData("xxyxx")]
        public void Solution_ExampleString_Nice(string @string)
        {
            Assert.Equal(1, task.Solution(@string));
        }

        [Theory]
        [InlineData("uurcxstgmygtbstg")]
        [InlineData("ieodomkazucvgmuy")]
        public void Solution_ExampleString_Naughty(string @string)
        {
            Assert.Equal(0, task.Solution(@string));
        }
    }
}
