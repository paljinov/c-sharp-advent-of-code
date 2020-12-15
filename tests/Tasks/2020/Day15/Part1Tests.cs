using System;
using App.Tasks.Year2020.Day15;
using Xunit;

namespace Tests.Tasks.Year2020.Day15
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstStartingNumbersExample_2020thSpokenNumberEquals()
        {
            string startingNumbers = "0,3,6";
            Assert.Equal(436, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_SecondStartingNumbersExample_2020thSpokenNumberEquals()
        {
            string startingNumbers = "1,3,2";
            Assert.Equal(1, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_ThirdStartingNumbersExample_2020thSpokenNumberEquals()
        {
            string startingNumbers = "2,1,3";
            Assert.Equal(10, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_FourthStartingNumbersExample_2020thSpokenNumberEquals()
        {
            string startingNumbers = "1,2,3";
            Assert.Equal(27, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_FifthStartingNumbersExample_2020thSpokenNumberEquals()
        {
            string startingNumbers = "2,3,1";
            Assert.Equal(78, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_SixthStartingNumbersExample_2020thSpokenNumberEquals()
        {
            string startingNumbers = "3,2,1";
            Assert.Equal(438, task.Solution(startingNumbers));
        }

        [Fact]
        public void Solution_SeventhStartingNumbersExample_2020thSpokenNumberEquals()
        {
            string startingNumbers = "3,1,2";
            Assert.Equal(1836, task.Solution(startingNumbers));
        }
    }
}
