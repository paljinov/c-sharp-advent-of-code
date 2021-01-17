using System;
using App.Tasks.Year2016.Day9;
using Xunit;

namespace Tests.Tasks.Year2016.Day9
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstFileExample_FileVersionTwoDecompressedLengthEquals()
        {
            Assert.Equal(9, task.Solution("(3x3)XYZ"));
        }

        [Fact]
        public void Solution_SecondFileExample_FileVersionTwoDecompressedLengthEquals()
        {
            Assert.Equal(20, task.Solution("X(8x2)(3x3)ABCY"));
        }

        [Fact]
        public void Solution_ThirdFileExample_FileVersionTwoDecompressedLengthEquals()
        {
            Assert.Equal(241920, task.Solution("(27x12)(20x12)(13x14)(7x10)(1x12)A"));
        }

        [Fact]
        public void Solution_FourthFileExample_FileVersionTwoDecompressedLengthEquals()
        {
            Assert.Equal(445, task.Solution("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN"));
        }
    }
}
