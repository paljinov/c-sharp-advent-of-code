using System;
using App.Tasks.Year2021.Day20;
using Xunit;

namespace Tests.Tasks.Year2021.Day20
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ImageEnhancementAlgorithmAndInputImageExample_LitPixelsCountInTheResultingImageAfterFiftyEnhancementsEquals()
        {
            string imageEnhancementAlgorithmAndInputImage =
                "..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..##"
                + $"{Environment.NewLine}#..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###"
                + $"{Environment.NewLine}.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#."
                + $"{Environment.NewLine}.#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#....."
                + $"{Environment.NewLine}.#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.."
                + $"{Environment.NewLine}...####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#....."
                + $"{Environment.NewLine}..##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#"
                + Environment.NewLine
                + $"{Environment.NewLine}#..#."
                + $"{Environment.NewLine}#...."
                + $"{Environment.NewLine}##..#"
                + $"{Environment.NewLine}..#.."
                + $"{Environment.NewLine}..###";

            Assert.Equal(3351, task.Solution(imageEnhancementAlgorithmAndInputImage));
        }
    }
}
