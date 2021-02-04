using App.Tasks.Year2017.Day1;
using Xunit;

namespace Tests.Tasks.Year2017.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("1212", 6)]
        [InlineData("1221", 0)]
        [InlineData("123425", 4)]
        [InlineData("123123", 12)]
        [InlineData("12131415", 4)]
        public void Solution_DigitsSequencesExample_CaptchaSolutionEquals(string digitsSequence, int captchaSolution)
        {
            Assert.Equal(captchaSolution, task.Solution(digitsSequence));
        }
    }
}
