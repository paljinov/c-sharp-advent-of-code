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

        [Theory]
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("X(8x2)(3x3)ABCY", 20)]
        [InlineData("(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920)]
        [InlineData("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]
        public void Solution_FileExample_FileVersionTwoDecompressedLengthEquals(
            string file,
            int fileDecompressedLength
        )
        {
            Assert.Equal(fileDecompressedLength, task.Solution(file));
        }
    }
}
