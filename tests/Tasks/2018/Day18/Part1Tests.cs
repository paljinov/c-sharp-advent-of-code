using System;
using App.Tasks.Year2018.Day18;
using Xunit;

namespace Tests.Tasks.Year2018.Day18
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_AreaExample_TotalResourceValueOfTheLumberCollectionAreaAfterTenMinutesEquals()
        {
            string area = ".#.#...|#."
                + $"{Environment.NewLine}.....#|##|"
                + $"{Environment.NewLine}.|..|...#."
                + $"{Environment.NewLine}..|#.....#"
                + $"{Environment.NewLine}#.#|||#|#|"
                + $"{Environment.NewLine}...#.||..."
                + $"{Environment.NewLine}.|....|..."
                + $"{Environment.NewLine}||...#|.#|"
                + $"{Environment.NewLine}|.||||..|."
                + $"{Environment.NewLine}...#.|..|.";

            Assert.Equal(1147, task.Solution(area));
        }
    }
}
