﻿/*
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
    class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            int longestRouteDistance = 0;

            var possibleRoutes = PossibleRoutes.GetPossibleRoutes(input);
            foreach (var route in possibleRoutes)
            {
                if (route.Value > longestRouteDistance)
                {
                    longestRouteDistance = route.Value;
                }
            }

            return longestRouteDistance;
        }
    }
}
