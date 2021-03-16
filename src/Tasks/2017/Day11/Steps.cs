using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day11
{
    public class Steps
    {
        public int CalculateFewestNumberOfStepsToReachChildProcess(List<Direction> pathDirections)
        {
            (int fewestNumberOfSteps, _) = CalculateNumberOfSteps(pathDirections);
            return fewestNumberOfSteps;
        }

        public int CalculateFurthestStepsEver(List<Direction> pathDirections)
        {
            (_, int furthestStepsEver) = CalculateNumberOfSteps(pathDirections);
            return furthestStepsEver;
        }

        private (int fewestNumberOfSteps, int furthestStepsEver) CalculateNumberOfSteps(List<Direction> pathDirections)
        {
            int x = 0;
            int y = 0;

            int furthestStepsEver = 0;

            foreach (Direction direction in pathDirections)
            {
                switch (direction)
                {
                    case Direction.North:
                        y++;
                        break;
                    case Direction.Northeast:
                        x++;
                        y++;
                        break;
                    case Direction.Northwest:
                        x--;
                        break;
                    case Direction.South:
                        y--;
                        break;
                    case Direction.Southeast:
                        x++;
                        break;
                    case Direction.Southwest:
                        x--;
                        y--;
                        break;
                }

                furthestStepsEver = new[] { furthestStepsEver, Math.Abs(x), Math.Abs(y) }.Max();
            }

            int fewestNumberOfSteps = Math.Max(Math.Abs(x), Math.Abs(y));

            return (fewestNumberOfSteps, furthestStepsEver);
        }
    }
}
