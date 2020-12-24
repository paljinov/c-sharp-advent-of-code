using System;
using App.Tasks.Year2020.Day24;
using Xunit;

namespace Tests.Tasks.Year2020.Day24
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_TilesExample_TilesWithBlackSideUpForAdjacentRulesAfterOneHundredDaysEquals()
        {
            string tiles = "sesenwnenenewseeswwswswwnenewsewsw"
                + $"{Environment.NewLine}neeenesenwnwwswnenewnwwsewnenwseswesw"
                + $"{Environment.NewLine}seswneswswsenwwnwse"
                + $"{Environment.NewLine}nwnwneseeswswnenewneswwnewseswneseene"
                + $"{Environment.NewLine}swweswneswnenwsewnwneneseenw"
                + $"{Environment.NewLine}eesenwseswswnenwswnwnwsewwnwsene"
                + $"{Environment.NewLine}sewnenenenesenwsewnenwwwse"
                + $"{Environment.NewLine}wenwwweseeeweswwwnwwe"
                + $"{Environment.NewLine}wsweesenenewnwwnwsenewsenwwsesesenwne"
                + $"{Environment.NewLine}neeswseenwwswnwswswnw"
                + $"{Environment.NewLine}nenwswwsewswnenenewsenwsenwnesesenew"
                + $"{Environment.NewLine}enewnwewneswsewnwswenweswnenwsenwsw"
                + $"{Environment.NewLine}sweneswneswneneenwnewenewwneswswnese"
                + $"{Environment.NewLine}swwesenesewenwneswnwwneseswwne"
                + $"{Environment.NewLine}enesenwswwswneneswsenwnewswseenwsese"
                + $"{Environment.NewLine}wnwnesenesenenwwnenwsewesewsesesew"
                + $"{Environment.NewLine}nenewswnwewswnenesenwnesewesw"
                + $"{Environment.NewLine}eneswnwswnwsenenwnwnwwseeswneewsenese"
                + $"{Environment.NewLine}neswnwewnwnwseenwseesewsenwsweewe"
                + $"{Environment.NewLine}wseweeenwnesenwwwswnew";

            Assert.Equal(2208, task.Solution(tiles));
        }
    }
}
