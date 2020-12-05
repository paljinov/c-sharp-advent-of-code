using System;
using App.Tasks.Year2020.Day5;
using Xunit;

namespace Tests.Tasks.Year2020.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstBoardingPassExample_SeatIdEquals()
        {
            Assert.Equal(357, task.Solution("FBFBBFFRLR"));
        }

        [Fact]
        public void Solution_SecondBoardingPassExample_SeatIdEquals()
        {
            Assert.Equal(567, task.Solution("BFFFBBFRRR"));
        }

        [Fact]
        public void Solution_ThirdBoardingPassExample_SeatIdEquals()
        {
            Assert.Equal(119, task.Solution("FFFBBBFRRR"));
        }

        [Fact]
        public void Solution_FourthBoardingPassExample_SeatIdEquals()
        {
            Assert.Equal(820, task.Solution("BBFFBBFRLL"));
        }
    }
}
