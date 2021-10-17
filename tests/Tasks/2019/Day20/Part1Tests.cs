using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2019.Day20;
using Xunit;

namespace Tests.Tasks.Year2019.Day20
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(MazeMap_StepsNeededToGetFromStartTileToEndTileCount_TestData))]
        public void Solution_MazeMapExample_StepsNeededToGetFromStartTileToEndTileCountEquals(
            string mazeMap,
            int stepsNeededToGetFromStartTileToEndTile
        )
        {
            Assert.Equal(stepsNeededToGetFromStartTileToEndTile, task.Solution(mazeMap));
        }

        public class MazeMap_StepsNeededToGetFromStartTileToEndTileCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "         A           "
                    + $"{Environment.NewLine}         A           "
                    + $"{Environment.NewLine}  #######.#########  "
                    + $"{Environment.NewLine}  #######.........#  "
                    + $"{Environment.NewLine}  #######.#######.#  "
                    + $"{Environment.NewLine}  #######.#######.#  "
                    + $"{Environment.NewLine}  #######.#######.#  "
                    + $"{Environment.NewLine}  #####  B    ###.#  "
                    + $"{Environment.NewLine}BC...##  C    ###.#  "
                    + $"{Environment.NewLine}  ##.##       ###.#  "
                    + $"{Environment.NewLine}  ##...DE  F  ###.#  "
                    + $"{Environment.NewLine}  #####    G  ###.#  "
                    + $"{Environment.NewLine}  #########.#####.#  "
                    + $"{Environment.NewLine}DE..#######...###.#  "
                    + $"{Environment.NewLine}  #.#########.###.#  "
                    + $"{Environment.NewLine}FG..#########.....#  "
                    + $"{Environment.NewLine}  ###########.#####  "
                    + $"{Environment.NewLine}             Z       "
                    + $"{Environment.NewLine}             Z       ",
                    23
                };

                yield return new object[] {
                    "                   A               "
                    + $"{Environment.NewLine}                   A               "
                    + $"{Environment.NewLine}  #################.#############  "
                    + $"{Environment.NewLine}  #.#...#...................#.#.#  "
                    + $"{Environment.NewLine}  #.#.#.###.###.###.#########.#.#  "
                    + $"{Environment.NewLine}  #.#.#.......#...#.....#.#.#...#  "
                    + $"{Environment.NewLine}  #.#########.###.#####.#.#.###.#  "
                    + $"{Environment.NewLine}  #.............#.#.....#.......#  "
                    + $"{Environment.NewLine}  ###.###########.###.#####.#.#.#  "
                    + $"{Environment.NewLine}  #.....#        A   C    #.#.#.#  "
                    + $"{Environment.NewLine}  #######        S   P    #####.#  "
                    + $"{Environment.NewLine}  #.#...#                 #......VT"
                    + $"{Environment.NewLine}  #.#.#.#                 #.#####  "
                    + $"{Environment.NewLine}  #...#.#               YN....#.#  "
                    + $"{Environment.NewLine}  #.###.#                 #####.#  "
                    + $"{Environment.NewLine}DI....#.#                 #.....#  "
                    + $"{Environment.NewLine}  #####.#                 #.###.#  "
                    + $"{Environment.NewLine}ZZ......#               QG....#..AS"
                    + $"{Environment.NewLine}  ###.###                 #######  "
                    + $"{Environment.NewLine}JO..#.#.#                 #.....#  "
                    + $"{Environment.NewLine}  #.#.#.#                 ###.#.#  "
                    + $"{Environment.NewLine}  #...#..DI             BU....#..LF"
                    + $"{Environment.NewLine}  #####.#                 #.#####  "
                    + $"{Environment.NewLine}YN......#               VT..#....QG"
                    + $"{Environment.NewLine}  #.###.#                 #.###.#  "
                    + $"{Environment.NewLine}  #.#...#                 #.....#  "
                    + $"{Environment.NewLine}  ###.###    J L     J    #.#.###  "
                    + $"{Environment.NewLine}  #.....#    O F     P    #.#...#  "
                    + $"{Environment.NewLine}  #.###.#####.#.#####.#####.###.#  "
                    + $"{Environment.NewLine}  #...#.#.#...#.....#.....#.#...#  "
                    + $"{Environment.NewLine}  #.#####.###.###.#.#.#########.#  "
                    + $"{Environment.NewLine}  #...#.#.....#...#.#.#.#.....#.#  "
                    + $"{Environment.NewLine}  #.###.#####.###.###.#.#.#######  "
                    + $"{Environment.NewLine}  #.#.........#...#.............#  "
                    + $"{Environment.NewLine}  #########.###.###.#############  "
                    + $"{Environment.NewLine}           B   J   C               "
                    + $"{Environment.NewLine}           U   P   P               ",
                    58
                };

                yield return new object[] {
                    "             Z L X W       C                 "
                    + $"{Environment.NewLine}             Z P Q B       K                 "
                    + $"{Environment.NewLine}  ###########.#.#.#.#######.###############  "
                    + $"{Environment.NewLine}  #...#.......#.#.......#.#.......#.#.#...#  "
                    + $"{Environment.NewLine}  ###.#.#.#.#.#.#.#.###.#.#.#######.#.#.###  "
                    + $"{Environment.NewLine}  #.#...#.#.#...#.#.#...#...#...#.#.......#  "
                    + $"{Environment.NewLine}  #.###.#######.###.###.#.###.###.#.#######  "
                    + $"{Environment.NewLine}  #...#.......#.#...#...#.............#...#  "
                    + $"{Environment.NewLine}  #.#########.#######.#.#######.#######.###  "
                    + $"{Environment.NewLine}  #...#.#    F       R I       Z    #.#.#.#  "
                    + $"{Environment.NewLine}  #.###.#    D       E C       H    #.#.#.#  "
                    + $"{Environment.NewLine}  #.#...#                           #...#.#  "
                    + $"{Environment.NewLine}  #.###.#                           #.###.#  "
                    + $"{Environment.NewLine}  #.#....OA                       WB..#.#..ZH"
                    + $"{Environment.NewLine}  #.###.#                           #.#.#.#  "
                    + $"{Environment.NewLine}CJ......#                           #.....#  "
                    + $"{Environment.NewLine}  #######                           #######  "
                    + $"{Environment.NewLine}  #.#....CK                         #......IC"
                    + $"{Environment.NewLine}  #.###.#                           #.###.#  "
                    + $"{Environment.NewLine}  #.....#                           #...#.#  "
                    + $"{Environment.NewLine}  ###.###                           #.#.#.#  "
                    + $"{Environment.NewLine}XF....#.#                         RF..#.#.#  "
                    + $"{Environment.NewLine}  #####.#                           #######  "
                    + $"{Environment.NewLine}  #......CJ                       NM..#...#  "
                    + $"{Environment.NewLine}  ###.#.#                           #.###.#  "
                    + $"{Environment.NewLine}RE....#.#                           #......RF"
                    + $"{Environment.NewLine}  ###.###        X   X       L      #.#.#.#  "
                    + $"{Environment.NewLine}  #.....#        F   Q       P      #.#.#.#  "
                    + $"{Environment.NewLine}  ###.###########.###.#######.#########.###  "
                    + $"{Environment.NewLine}  #.....#...#.....#.......#...#.....#.#...#  "
                    + $"{Environment.NewLine}  #####.#.###.#######.#######.###.###.#.#.#  "
                    + $"{Environment.NewLine}  #.......#.......#.#.#.#.#...#...#...#.#.#  "
                    + $"{Environment.NewLine}  #####.###.#####.#.#.#.#.###.###.#.###.###  "
                    + $"{Environment.NewLine}  #.......#.....#.#...#...............#...#  "
                    + $"{Environment.NewLine}  #############.#.#.###.###################  "
                    + $"{Environment.NewLine}               A O F   N                     "
                    + $"{Environment.NewLine}               A A D   M                     ",
                    77
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
