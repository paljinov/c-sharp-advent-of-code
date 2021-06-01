/*
--- Part Two ---

The next year, just to show off, Santa decides to take the route with the
longest distance instead.

He can still start and end at any two (different) locations he wants, and he
still must visit each location exactly once.

For example, given the distances above, the longest route would be 982 via (for
example) Dublin -> London -> Belfast.

What is the distance of the longest route?
*/

namespace App.Tasks.Year2015.Day9
{
    public class Part2 : ITask<int>
    {
        private readonly DistancesRepository distancesRepository;

        private readonly PossibleRoutes possibleRoutes;

        public Part2()
        {
            distancesRepository = new DistancesRepository();
            possibleRoutes = new PossibleRoutes();
        }

        public int Solution(string input)
        {
            LocationsDistance[] distances = distancesRepository.GetDistances(input);
            int longestRouteDistance = possibleRoutes.CalculateDistanceOfTheLongestRoute(distances);

            return longestRouteDistance;
        }
    }
}
