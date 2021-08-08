using System;
using App.Tasks.Year2019.Day10;
using Xunit;

namespace Tests.Tasks.Year2019.Day10
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_AsteroidMapExample_ResultForTwoHundredVaporizedAsteroidEquals()
        {
            string asteroidMap = ".#....#####...#.."
                + $"{Environment.NewLine}##...##.#####..##"
                + $"{Environment.NewLine}##...#...#.#####."
                + $"{Environment.NewLine}..#.....#...###.."
                + $"{Environment.NewLine}..#.#.....#....##";

            Assert.Equal(802, task.Solution(asteroidMap));
        }
    }
}
