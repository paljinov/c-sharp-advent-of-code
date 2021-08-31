using App.Tasks.Year2019.Day16;
using Xunit;

namespace Tests.Tasks.Year2019.Day16
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("03036732577212944063491565474664", "84462026")]
        [InlineData("02935109699940807407585447034323", "78725270")]
        [InlineData("03081770884921959731165446850517", "53553731")]
        public void Solution_InputSignalExample_EightDigitMessageEmbeddedInTheFinalOutputListEquals(
            string inputSignal,
            string eightDigitMessageEmbeddedInTheFinalOutputList
        )
        {
            Assert.Equal(eightDigitMessageEmbeddedInTheFinalOutputList, task.Solution(inputSignal));
        }
    }
}
