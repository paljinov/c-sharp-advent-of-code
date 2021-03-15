using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day11
{
    public class Steps
    {
        public int CalculateFewestNumberOfStepsToReachChildProcess(List<Direction> pathDirections)
        {
            int x = 0;
            int y = 0;

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
            }

            int fewestNumberOfSteps = Math.Max(Math.Abs(x), Math.Abs(y));

            return fewestNumberOfSteps;
        }
    }
}
