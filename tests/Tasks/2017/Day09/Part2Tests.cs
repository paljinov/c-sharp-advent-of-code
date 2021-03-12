using App.Tasks.Year2017.Day9;
using Xunit;

namespace Tests.Tasks.Year2017.Day9
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("<>", 0)]
        [InlineData("<random characters>", 17)]
        [InlineData("<<<<>", 3)]
        [InlineData("<{!>}>", 2)]
        [InlineData("<!!>", 0)]
        [InlineData("<!!!>>", 0)]
        [InlineData("<{o\"i!a,<{i<a>", 10)]
        public void Solution_StreamExample_NonCanceledCharactersWithinGarbageEquals(
            string stream,
            int nonCanceledCharactersWithinGarbage
        )
        {
            Assert.Equal(nonCanceledCharactersWithinGarbage, task.Solution(stream));
        }
    }
}
