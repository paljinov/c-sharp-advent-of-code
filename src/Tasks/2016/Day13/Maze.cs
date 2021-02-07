using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day13
{
    public class Maze
    {
        private const int START_X = 1;

        private const int START_Y = 1;

        public int CalculateFewestNumberOfStepsToReachCoordinates(int favoriteNumber, int destinationX, int destinationY)
        {
            Dictionary<(int x, int y), int> visitedCoordinatesInSteps = new Dictionary<(int x, int y), int>();
            int minSteps = int.MaxValue;
            GetFewestNumberOfSteps(
                favoriteNumber, destinationX, destinationY, START_X, START_Y, 0, ref minSteps, visitedCoordinatesInSteps
            );

            return minSteps;
        }

        private void GetFewestNumberOfSteps(
            int favoriteNumber,
            int destinationX,
            int destinationY,
            int x,
            int y,
            int steps,
            ref int minSteps,
            Dictionary<(int x, int y), int> visitedCoordinatesInSteps
        )
        {
            if (steps >= minSteps)
            {
                return;
            }

            if (x == destinationX && y == destinationY)
            {
                minSteps = Math.Min(steps, minSteps);
                return;
            }

            if (visitedCoordinatesInSteps.ContainsKey((x, y)) && steps >= visitedCoordinatesInSteps[(x, y)])
            {
                return;
            }

            visitedCoordinatesInSteps[(x, y)] = steps;

            steps += 1;

            if (IsOpenSpace(favoriteNumber, x + 1, y))
            {
                GetFewestNumberOfSteps(
                    favoriteNumber, destinationX, destinationY, x + 1, y, steps, ref minSteps, visitedCoordinatesInSteps
                );
            }

            if (IsOpenSpace(favoriteNumber, x, y + 1))
            {
                GetFewestNumberOfSteps(
                    favoriteNumber, destinationX, destinationY, x, y + 1, steps, ref minSteps, visitedCoordinatesInSteps
                );
            }


            if (x > 0)
            {
                if (IsOpenSpace(favoriteNumber, x - 1, y))
                {
                    GetFewestNumberOfSteps(
                        favoriteNumber, destinationX, destinationY, x - 1, y, steps, ref minSteps, visitedCoordinatesInSteps
                    );
                }
            }

            if (y > 0)
            {
                if (IsOpenSpace(favoriteNumber, x, y - 1))
                {
                    GetFewestNumberOfSteps(
                        favoriteNumber, destinationX, destinationY, x, y - 1, steps, ref minSteps, visitedCoordinatesInSteps
                    );
                }
            }
        }

        private bool IsOpenSpace(int favoriteNumber, int x, int y)
        {
            int result = CalculateFormulaResult(favoriteNumber, x, y);
            string binary = Convert.ToString(result, 2);
            bool isNumberOfOneBitsEven = IsNumberOfOneBitsEven(binary);

            return isNumberOfOneBitsEven;
        }

        private int CalculateFormulaResult(int favoriteNumber, int x, int y)
        {
            int result = x * x + 3 * x + 2 * x * y + y + y * y + favoriteNumber;
            return result;
        }

        private bool IsNumberOfOneBitsEven(string binary)
        {
            int oneBits = binary.Count(bit => bit == '1');
            if (oneBits % 2 == 0)
            {
                return true;
            }

            return false;
        }
    }
}
