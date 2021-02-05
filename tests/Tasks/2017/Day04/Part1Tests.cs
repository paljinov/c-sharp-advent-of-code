using App.Tasks.Year2017.Day4;
using Xunit;

namespace Tests.Tasks.Year2017.Day4
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("aa bb cc dd ee", 1)]
        [InlineData("aa bb cc dd aa", 0)]
        [InlineData("aa bb cc dd aaa", 1)]
        public void Solution_PassphrasesExample_ValidPassphrasesForDuplicateWordsConditionEquals(
            string passphrases,
            int validPassphrases
        )
        {
            Assert.Equal(validPassphrases, task.Solution(passphrases));
        }
    }
}
