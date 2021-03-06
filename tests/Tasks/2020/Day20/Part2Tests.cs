using System;
using App.Tasks.Year2020.Day20;
using Xunit;

namespace Tests.Tasks.Year2020.Day20
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_TilesExample_HashSignsWhichAreNotPartOfSeaMonsterEquals()
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

            Assert.Equal(273, task.Solution(tiles));
        }
    }
}
