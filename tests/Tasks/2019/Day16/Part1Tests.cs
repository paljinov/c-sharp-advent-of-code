using App.Tasks.Year2019.Day16;
using Xunit;

namespace Tests.Tasks.Year2019.Day16
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("80871224585914546619083218645595", "24176176")]
        [InlineData("19617804207202209144916044189917", "73745418")]
        [InlineData("69317163492948606335995924319873", "52432133")]
        public void Solution_InputSignalExample_FirstEightDigitsInTheFinalOutputListEquals(
            string inputSignal,
            string firstEightDigitsInTheFinalOutputList
        )
        {
            Assert.Equal(firstEightDigitsInTheFinalOutputList, task.Solution(inputSignal));
        }
    }
}
