using System;
using App.Tasks.Year2020.Day3;
using Xunit;

namespace Tests.Tasks.Year2020.Day3
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ExampleAreaMap_EncounteredTreesEquals()
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

            Assert.Equal(7, task.Solution(areaString));
        }
    }
}
