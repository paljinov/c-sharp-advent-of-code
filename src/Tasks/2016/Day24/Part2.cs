/*
--- Part Two ---

Of course, if you leave the cleaning robot somewhere weird, someone is bound to
notice.

What is the fewest number of steps required to start at 0, visit every non-0
number marked on the map at least once, and then return to 0?
*/

namespace App.Tasks.Year2016.Day24
{
    public class Part2 : ITask<int>
    {
        private readonly MapRepository mapRepository;

        private readonly Route route;

        public Part2()
        {
            mapRepository = new MapRepository();
            route = new Route();
        }

        public int Solution(string input)
        {
            char[,] map = mapRepository.GetMap(input);
            int fewestNumberOfSteps = route.CalculateFewestNumberOfStepsRequiredToVisitEveryNonZeroNumber(map, true);

            return fewestNumberOfSteps;
        }
    }
}
