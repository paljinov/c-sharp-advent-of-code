using System;
using App.Tasks.Year2020.Day3;
using Xunit;

namespace Tests.Tasks.Year2020.Day3
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ExampleAreaMap_EncounteredTreesMultiplicationEquals()
        {
            string areaString = "..##......."
                + $"{Environment.NewLine}#...#...#.."
                + $"{Environment.NewLine}.#....#..#."
                + $"{Environment.NewLine}..#.#...#.#"
                + $"{Environment.NewLine}.#...##..#."
                + $"{Environment.NewLine}..#.##....."
                + $"{Environment.NewLine}.#.#.#....#"
                + $"{Environment.NewLine}.#........#"
                + $"{Environment.NewLine}#.##...#..."
                + $"{Environment.NewLine}#...##....#"
                + $"{Environment.NewLine}.#..#...#.#";

            Assert.Equal(336, task.Solution(areaString));
        }
    }
}
