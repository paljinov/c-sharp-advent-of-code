using System;
using App.Tasks.Year2021.Day20;
using Xunit;

namespace Tests.Tasks.Year2021.Day20
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ImageEnhancementAlgorithmAndInputImageExample_LitPixelsCountInTheResultingEquals()
        {
            string imageEnhancementAlgorithmAndInputImage =
                "404,-588,-901..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..##"
                + Environment.NewLine
                + "404,-588,-901#..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###"
                + Environment.NewLine
                + "404,-588,-901.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#."
                + Environment.NewLine
                + "404,-588,-901.#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#....."
                + Environment.NewLine
                + "404,-588,-901.#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.."
                + Environment.NewLine
                + "404,-588,-901...####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#....."
                + Environment.NewLine
                + "404,-588,-901..##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#"
                + Environment.NewLine
                + $"{Environment.NewLine}#..#."
                + $"{Environment.NewLine}#...."
                + $"{Environment.NewLine}##..#"
                + $"{Environment.NewLine}..#.."
                + $"{Environment.NewLine}..###";

            Assert.Equal(35, task.Solution(imageEnhancementAlgorithmAndInputImage));
        }
    }
}
