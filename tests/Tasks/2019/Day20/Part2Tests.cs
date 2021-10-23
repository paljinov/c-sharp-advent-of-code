using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2019.Day20;
using Xunit;

namespace Tests.Tasks.Year2019.Day20
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [ClassData(typeof(MazeMap_StepsNeededToGetFromStartTileToEndTileAtTheOutermostLayerCount_TestData))]
        public void Solution_MazeMapExample_StepsNeededToGetFromStartTileToEndTileAtTheOutermostLayerCountEquals(
            string mazeMap,
            int stepsNeededToGetFromStartTileToEndTileAtTheOutermostLayer
        )
        {
            Assert.Equal(stepsNeededToGetFromStartTileToEndTileAtTheOutermostLayer, task.Solution(mazeMap));
        }

        public class MazeMap_StepsNeededToGetFromStartTileToEndTileAtTheOutermostLayerCount_TestData
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
                    26
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
                    396
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
