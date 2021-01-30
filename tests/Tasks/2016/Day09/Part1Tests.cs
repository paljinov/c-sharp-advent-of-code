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

        [Theory]
        [InlineData("ADVENT", 6)]
        [InlineData("A(1x5)BC", 7)]
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("A(2x2)BCD(2x2)EFG", 11)]
        [InlineData("(6x1)(1x3)A", 6)]
        [InlineData("X(8x2)(3x3)ABCY", 18)]
        public void Solution_FileExample_FileVersionOneDecompressedLengthEquals(
            string file,
            int fileDecompressedLength
        )
        {
            Assert.Equal(fileDecompressedLength, task.Solution(file));
        }
    }
}
