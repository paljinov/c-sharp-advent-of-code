using App.Tasks.Year2015.Day8;
using Xunit;

namespace Tests.Tasks.Year2015.Day8
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("\"\"", 2)]
        [InlineData("\"abc\"", 2)]
        [InlineData("\"aaa\\\"aaa\"", 3)]
        [InlineData("\"\\x27\"", 5)]
        public void Solution_CodeCharactersExample_StringLiteralsMinusStringMemoryEquals(
            string codeCharacters,
            int stringLiteralsMinusStringMemory
        )
        {
            Assert.Equal(stringLiteralsMinusStringMemory, task.Solution(codeCharacters));
        }
    }
}
