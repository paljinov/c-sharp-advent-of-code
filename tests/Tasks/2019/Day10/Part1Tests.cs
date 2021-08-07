using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2019.Day10;
using Xunit;

namespace Tests.Tasks.Year2019.Day10
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(AsteroidMap_NumberOfAsteroidsWhichCanBeDetectedFromMonitoringStationCount_TestData))]
        public void Solution_AsteroidMapExample_NumberOfAsteroidsWhichCanBeDetectedFromMonitoringStationCountEquals(
             string asteroidMap,
            long numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation
        )
        {
            Assert.Equal(numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation, task.Solution(asteroidMap));
        }

        public class AsteroidMap_NumberOfAsteroidsWhichCanBeDetectedFromMonitoringStationCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    ".#..#"
                    + $"{Environment.NewLine}....."
                    + $"{Environment.NewLine}#####"
                    + $"{Environment.NewLine}....#"
                    + $"{Environment.NewLine}...##",
                    8
                };

                yield return new object[] {
                    "......#.#."
                    + $"{Environment.NewLine}#..#.#...."
                    + $"{Environment.NewLine}..#######."
                    + $"{Environment.NewLine}.#.#.###.."
                    + $"{Environment.NewLine}.#..#....."
                    + $"{Environment.NewLine}..#....#.#"
                    + $"{Environment.NewLine}#..#....#."
                    + $"{Environment.NewLine}.##.#..###"
                    + $"{Environment.NewLine}##...#..#."
                    + $"{Environment.NewLine}.#....####",
                    33
                };

                yield return new object[] {
                    "#.#...#.#."
                    + $"{Environment.NewLine}.###....#."
                    + $"{Environment.NewLine}.#....#..."
                    + $"{Environment.NewLine}##.#.#.#.#"
                    + $"{Environment.NewLine}....#.#.#."
                    + $"{Environment.NewLine}.##..###.#"
                    + $"{Environment.NewLine}..#...##.."
                    + $"{Environment.NewLine}..##....##"
                    + $"{Environment.NewLine}......#..."
                    + $"{Environment.NewLine}.####.###.",
                    35
                };

                yield return new object[] {
                    ".#..#..###"
                    + $"{Environment.NewLine}####.###.#"
                    + $"{Environment.NewLine}....###.#."
                    + $"{Environment.NewLine}..###.##.#"
                    + $"{Environment.NewLine}##.##.#.#."
                    + $"{Environment.NewLine}....###..#"
                    + $"{Environment.NewLine}..#.#..#.#"
                    + $"{Environment.NewLine}#..#.#.###"
                    + $"{Environment.NewLine}.##...##.#"
                    + $"{Environment.NewLine}.....#.#..",
                    41
                };

                yield return new object[] {
                    ".#..##.###...#######"
                    + $"{Environment.NewLine}##.############..##."
                    + $"{Environment.NewLine}.#.######.########.#"
                    + $"{Environment.NewLine}.###.#######.####.#."
                    + $"{Environment.NewLine}#####.##.#.##.###.##"
                    + $"{Environment.NewLine}..#####..#.#########"
                    + $"{Environment.NewLine}####################"
                    + $"{Environment.NewLine}#.####....###.#.#.##"
                    + $"{Environment.NewLine}##.#################"
                    + $"{Environment.NewLine}#####.##.###..####.."
                    + $"{Environment.NewLine}..######..##.#######"
                    + $"{Environment.NewLine}####.##.####...##..#"
                    + $"{Environment.NewLine}.#####..#.######.###"
                    + $"{Environment.NewLine}##...#.##########..."
                    + $"{Environment.NewLine}#.##########.#######"
                    + $"{Environment.NewLine}.####.#.###.###.#.##"
                    + $"{Environment.NewLine}....##.##.###..#####"
                    + $"{Environment.NewLine}.#.#.###########.###"
                    + $"{Environment.NewLine}#.#.#.#####.####.###"
                    + $"{Environment.NewLine}###.##.####.##.#..##",
                    210
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
