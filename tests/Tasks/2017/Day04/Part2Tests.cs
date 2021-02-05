using App.Tasks.Year2017.Day4;
using Xunit;

namespace Tests.Tasks.Year2017.Day4
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("abcde fghij", 1)]
        [InlineData("abcde xyz ecdab", 0)]
        [InlineData("a ab abc abd abf abj", 1)]
        [InlineData("iiii oiii ooii oooi oooo", 1)]
        [InlineData("oiii ioii iioi iiio", 0)]
        public void Solution_PassphrasesExample_ValidPassphrasesForAnagramsConditionEquals(
            string passphrases,
            int validPassphrases
        )
        {
            Assert.Equal(validPassphrases, task.Solution(passphrases));
        }
    }
}
