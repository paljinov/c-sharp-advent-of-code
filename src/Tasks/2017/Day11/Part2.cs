/*
--- Part Two ---

How many steps away is the furthest he ever got from his starting position?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2017.Day11
{
    public class Part2 : ITask<int>
    {
        private readonly PathRepository pathRepository;

        private readonly Steps steps;

        public Part2()
        {
            pathRepository = new PathRepository();
            steps = new Steps();
        }

        public int Solution(string input)
        {
            List<Direction> pathDirections = pathRepository.GetPathDirections(input);
            int furthestStepsEver = steps.CalculateFurthestStepsEver(pathDirections);

            return furthestStepsEver;
        }
    }
}
