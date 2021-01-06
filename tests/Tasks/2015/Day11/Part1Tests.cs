using App.Tasks.Year2015.Day11;
using Xunit;

namespace Tests.Tasks.Year2015.Day11
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstCurrentPasswordExample_NextPasswordEquals()
        {
            Assert.Equal("abcdffaa", task.Solution("abcdefgh"));
        }

        [Fact]
        public void Solution_SecondCurrentPasswordExample_NextPasswordEquals()
        {
            Assert.Equal("ghjaabcc", task.Solution("ghijklmn"));
        }
    }
}
