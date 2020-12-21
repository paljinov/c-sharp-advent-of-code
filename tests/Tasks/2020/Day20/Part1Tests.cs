using System;
using App.Tasks.Year2020.Day20;
using Xunit;

namespace Tests.Tasks.Year2020.Day20
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_TilesExample_CornerTilesIdsProductEquals()
        {
            string tiles = "Tile 2311:"
                + $"{Environment.NewLine}..##.#..#."
                + $"{Environment.NewLine}##..#....."
                + $"{Environment.NewLine}#...##..#."
                + $"{Environment.NewLine}####.#...#"
                + $"{Environment.NewLine}##.##.###."
                + $"{Environment.NewLine}##...#.###"
                + $"{Environment.NewLine}.#.#.#..##"
                + $"{Environment.NewLine}..#....#.."
                + $"{Environment.NewLine}###...#.#."
                + $"{Environment.NewLine}..###..###"
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 1951:"
                + $"{Environment.NewLine}#.##...##."
                + $"{Environment.NewLine}#.####...#"
                + $"{Environment.NewLine}.....#..##"
                + $"{Environment.NewLine}#...######"
                + $"{Environment.NewLine}.##.#....#"
                + $"{Environment.NewLine}.###.#####"
                + $"{Environment.NewLine}###.##.##."
                + $"{Environment.NewLine}.###....#."
                + $"{Environment.NewLine}..#.#..#.#"
                + $"{Environment.NewLine}#...##.#.."
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 1171:"
                + $"{Environment.NewLine}####...##."
                + $"{Environment.NewLine}#..##.#..#"
                + $"{Environment.NewLine}##.#..#.#."
                + $"{Environment.NewLine}.###.####."
                + $"{Environment.NewLine}..###.####"
                + $"{Environment.NewLine}.##....##."
                + $"{Environment.NewLine}.#...####."
                + $"{Environment.NewLine}#.##.####."
                + $"{Environment.NewLine}####..#..."
                + $"{Environment.NewLine}.....##..."
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 1427:"
                + $"{Environment.NewLine}###.##.#.."
                + $"{Environment.NewLine}.#..#.##.."
                + $"{Environment.NewLine}.#.##.#..#"
                + $"{Environment.NewLine}#.#.#.##.#"
                + $"{Environment.NewLine}....#...##"
                + $"{Environment.NewLine}...##..##."
                + $"{Environment.NewLine}...#.#####"
                + $"{Environment.NewLine}.#.####.#."
                + $"{Environment.NewLine}..#..###.#"
                + $"{Environment.NewLine}..##.#..#."
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 1489:"
                + $"{Environment.NewLine}##.#.#...."
                + $"{Environment.NewLine}..##...#.."
                + $"{Environment.NewLine}.##..##..."
                + $"{Environment.NewLine}..#...#..."
                + $"{Environment.NewLine}#####...#."
                + $"{Environment.NewLine}#..#.#.#.#"
                + $"{Environment.NewLine}...#.#.#.."
                + $"{Environment.NewLine}##.#...##."
                + $"{Environment.NewLine}..##.##.##"
                + $"{Environment.NewLine}###.##.#.."
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 2473:"
                + $"{Environment.NewLine}#....####."
                + $"{Environment.NewLine}#..#.##..."
                + $"{Environment.NewLine}#.##..#..."
                + $"{Environment.NewLine}######.#.#"
                + $"{Environment.NewLine}.#...#.#.#"
                + $"{Environment.NewLine}.#########"
                + $"{Environment.NewLine}.###.#..#."
                + $"{Environment.NewLine}########.#"
                + $"{Environment.NewLine}##...##.#."
                + $"{Environment.NewLine}..###.#.#."
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 2971:"
                + $"{Environment.NewLine}..#.#....#"
                + $"{Environment.NewLine}#...###..."
                + $"{Environment.NewLine}#.#.###..."
                + $"{Environment.NewLine}##.##..#.."
                + $"{Environment.NewLine}.#####..##"
                + $"{Environment.NewLine}.#..####.#"
                + $"{Environment.NewLine}#..#.#..#."
                + $"{Environment.NewLine}..####.###"
                + $"{Environment.NewLine}..#.#.###."
                + $"{Environment.NewLine}...#.#.#.#"
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 2729:"
                + $"{Environment.NewLine}...#.#.#.#"
                + $"{Environment.NewLine}####.#...."
                + $"{Environment.NewLine}..#.#....."
                + $"{Environment.NewLine}....#..#.#"
                + $"{Environment.NewLine}.##..##.#."
                + $"{Environment.NewLine}.#.####..."
                + $"{Environment.NewLine}####.#.#.."
                + $"{Environment.NewLine}##.####..."
                + $"{Environment.NewLine}##..#.##.."
                + $"{Environment.NewLine}#.##...##."
                + Environment.NewLine
                + $"{Environment.NewLine}Tile 3079:"
                + $"{Environment.NewLine}#.#.#####."
                + $"{Environment.NewLine}.#..######"
                + $"{Environment.NewLine}..#......."
                + $"{Environment.NewLine}######...."
                + $"{Environment.NewLine}####.#..#."
                + $"{Environment.NewLine}.#...#.##."
                + $"{Environment.NewLine}#.#####.##"
                + $"{Environment.NewLine}..#.###..."
                + $"{Environment.NewLine}..#......."
                + $"{Environment.NewLine}..#.###...";

            Assert.Equal(20899048083289, task.Solution(tiles));
        }
    }
}
