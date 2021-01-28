using App.Tasks.Year2015.Day8;
using Xunit;

namespace Tests.Tasks.Year2015.Day8
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("\"\"", 4)]
        [InlineData("\"abc\"", 4)]
        [InlineData("\"aaa\\\"aaa\"", 6)]
        [InlineData("\"\\x27\"", 5)]
        public void Solution_CodeCharactersExample_EncodedStringMinusOriginalStringLiteralsEquals(
            string codeCharacters,
            int encodedStringMinusOriginalStringLiterals
        )
        {
            Assert.Equal(encodedStringMinusOriginalStringLiterals, task.Solution(codeCharacters));
        }
    }
}
