using App.Tasks.Year2020.Day15;
using Xunit;

namespace Tests.Tasks.Year2020.Day15
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstStartingNumbersExample_30000000thSpokenNumberEquals()
        {
            string startingNumbers = "0,3,6";
            Assert.Equal(175594, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_SecondStartingNumbersExample_30000000thSpokenNumberEquals()
        {
            string startingNumbers = "1,3,2";
            Assert.Equal(2578, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_ThirdStartingNumbersExample_30000000thSpokenNumberEquals()
        {
            string startingNumbers = "2,1,3";
            Assert.Equal(3544142, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_FourthStartingNumbersExample_30000000thSpokenNumberEquals()
        {
            string startingNumbers = "1,2,3";
            Assert.Equal(261214, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_FifthStartingNumbersExample_30000000thSpokenNumberEquals()
        {
            string startingNumbers = "2,3,1";
            Assert.Equal(6895259, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_SixthStartingNumbersExample_30000000thSpokenNumberEquals()
        {
            string startingNumbers = "3,2,1";
            Assert.Equal(18, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_SeventhStartingNumbersExample_30000000thSpokenNumberEquals()
        {
            string startingNumbers = "3,1,2";
            Assert.Equal(362, task.Solution(startingNumbers));
        }
    }
}
