/*
--- Part Two ---

You flip the instructions over; Santa goes on to point out that this is all just
an implementation of Conway's Game of Life. At least, it was, until you notice
that something's wrong with the grid of lights you bought: four lights, one in
each corner, are stuck on and can't be turned off. The example above will
actually run like this:

Initial state:
##.#.#
...##.
#....#
..#...
#.#..#
####.#

After 1 step:
#.##.#
####.#
...##.
......
#...#.
#.####

After 2 steps:
#..#.#
#....#
.#.##.
...##.
.#..##
##.###

After 3 steps:
#...##
####.#
..##.#
......
##....
####.#

After 4 steps:
#.####
#....#
...#..
.##...
#.....
#.#..#

After 5 steps:
##.###
.##..#
.##...
.##...
#.#...
##...#

After 5 steps, this example now has 17 lights on.

In your grid of 100x100 lights, given your initial configuration, but with the
four corners always in the on state, how many lights are on after 100 steps?
*/

namespace App.Tasks.Year2015.Day18
{
    class Part2 : ITask<int>
    {
        private readonly InitialLightsConfigurationRepository initialLightsConfigurationRepository;

        private readonly LightsGrid lightsGrid;

        public Part2()
        {
            initialLightsConfigurationRepository = new InitialLightsConfigurationRepository();
            lightsGrid = new LightsGrid();
        }

        public int Solution(string input)
        {
            bool[,] lightsGrid = initialLightsConfigurationRepository.ParseInput(input);

            int rows = lightsGrid.GetLength(0);
            int columns = lightsGrid.GetLength(1);
            // Corners lights are stuck on
            lightsGrid[0, 0] = true;
            lightsGrid[0, columns - 1] = true;
            lightsGrid[rows - 1, 0] = true;
            lightsGrid[rows - 1, columns - 1] = true;

            int lightsTurnedOn = this.lightsGrid.CalculateLightsOnAfterAnimation(lightsGrid, true);

            return lightsTurnedOn;
        }
    }
}
