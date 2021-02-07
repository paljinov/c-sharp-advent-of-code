/*
--- Part Two ---

How many locations (distinct x,y coordinates, including your starting location)
can you reach in at most 50 steps?
*/

namespace App.Tasks.Year2016.Day13
{
    public class Part2 : ITask<int>
    {
        private const int MOST_STEPS = 50;

        private readonly FavoriteNumberRepository favoriteNumberRepository;

        private readonly Maze maze;

        public Part2()
        {
            favoriteNumberRepository = new FavoriteNumberRepository();
            maze = new Maze();
        }

        public int Solution(string input)
        {
            int favoriteNumber = favoriteNumberRepository.GetFavoriteNumber(input);
            int differentLocationsVisitedInSteps = maze.CalculateDifferentLocationsVisitedAtMostSteps(
                favoriteNumber,
                MOST_STEPS
            );

            return differentLocationsVisitedInSteps;
        }
    }
}
