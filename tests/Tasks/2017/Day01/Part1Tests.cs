using App.Tasks.Year2017.Day1;
using Xunit;

namespace Tests.Tasks.Year2017.Day1
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("1122", 3)]
        [InlineData("1111", 4)]
        [InlineData("1234", 0)]
        [InlineData("91212129", 9)]
        public void Solution_DigitsSequences_CaptchaSolutionEquals(string digitsSequence, int captchaSolution)
        {
            Assert.Equal(captchaSolution, task.Solution(digitsSequence));
        }
    }
}
