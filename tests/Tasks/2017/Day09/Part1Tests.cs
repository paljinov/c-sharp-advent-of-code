using App.Tasks.Year2017.Day9;
using Xunit;

namespace Tests.Tasks.Year2017.Day9
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("{}", 1)]
        [InlineData("{{{}}}", 6)]
        [InlineData("{{},{}}", 5)]
        [InlineData("{{{},{},{{}}}}", 16)]
        [InlineData("{<a>,<a>,<a>,<a>}", 1)]
        [InlineData("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
        [InlineData("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
        [InlineData("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
        public void Solution_StreamExample_AllGroupsTotalScoreEquals(string stream, int allGroupsTotalScore)
        {
            Assert.Equal(allGroupsTotalScore, task.Solution(stream));
        }
    }
}
