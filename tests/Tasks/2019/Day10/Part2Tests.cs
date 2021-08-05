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

        [Theory]
        [InlineData(
            @".#....#####...#..
            ##...##.#####..##
            ##...#...#.#####.
            ..#.....X...###..
            ..#.#.....#....##", 802)]
        public void Solution_AsteroidMapExample_ResultForTwoHundredVaporizedAsteroidEquals(
            string asteroidMap,
            long result
        )
        {
            Assert.Equal(result, task.Solution(asteroidMap));
        }
    }
}
