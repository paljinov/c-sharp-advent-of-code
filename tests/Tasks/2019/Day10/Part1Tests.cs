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
        [InlineData(
            @".#..#
            .....
            #####
            ....#
            ...##", 8)]
        [InlineData(
            @"......#.#.
            #..#.#....
            ..#######.
            .#.#.###..
            .#..#.....
            ..#....#.#
            #..#....#.
            .##.#..###
            ##...#..#.
            .#....####", 33)]
        [InlineData(
            @"#.#...#.#.
            .###....#.
            .#....#...
            ##.#.#.#.#
            ....#.#.#.
            .##..###.#
            ..#...##..
            ..##....##
            ......#...
            .####.###.", 35)]
        [InlineData(
            @".#..#..###
            ####.###.#
            ....###.#.
            ..###.##.#
            ##.##.#.#.
            ....###..#
            ..#.#..#.#
            #..#.#.###
            .##...##.#
            .....#.#..", 41)]
        [InlineData(
            @".#..##.###...#######
            ##.############..##.
            .#.######.########.#
            .###.#######.####.#.
            #####.##.#.##.###.##
            ..#####..#.#########
            ####################
            #.####....###.#.#.##
            ##.#################
            #####.##.###..####..
            ..######..##.#######
            ####.##.####...##..#
            .#####..#.######.###
            ##...#.##########...
            #.##########.#######
            .####.#.###.###.#.##
            ....##.##.###..#####
            .#.#.###########.###
            #.#.#.#####.####.###
            ###.##.####.##.#..##", 210)]
        public void Solution_AsteroidMapExample_NumberOfAsteroidsWhichCanBeDetectedFromMonitoringStationCountEquals(
            string asteroidMap,
            long numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation
        )
        {
            Assert.Equal(numberOfAsteroidsWhichCanBeDetectedFromMonitoringStation, task.Solution(asteroidMap));
        }
    }
}
