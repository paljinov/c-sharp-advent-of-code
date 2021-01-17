using System;
using App.Tasks.Year2016.Day9;
using Xunit;

namespace Tests.Tasks.Year2016.Day9
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstFileExample_FileVersionOneDecompressedLengthEquals()
        {
            Assert.Equal(6, task.Solution("ADVENT"));
        }

        [Fact]
        public void Solution_SecondFileExample_FileVersionOneDecompressedLengthEquals()
        {
            Assert.Equal(7, task.Solution("A(1x5)BC"));
        }

        [Fact]
        public void Solution_ThirdFileExample_FileVersionOneDecompressedLengthEquals()
        {
            Assert.Equal(9, task.Solution("(3x3)XYZ"));
        }

        [Fact]
        public void Solution_FourthFileExample_FileVersionOneDecompressedLengthEquals()
        {
            Assert.Equal(11, task.Solution("A(2x2)BCD(2x2)EFG"));
        }

        [Fact]
        public void Solution_FifthFileExample_FileVersionOneDecompressedLengthEquals()
        {
            Assert.Equal(6, task.Solution("(6x1)(1x3)A"));
        }

        [Fact]
        public void Solution_SixthFileExample_FileVersionOneDecompressedLengthEquals()
        {
            Assert.Equal(18, task.Solution("X(8x2)(3x3)ABCY"));
        }
    }
}
