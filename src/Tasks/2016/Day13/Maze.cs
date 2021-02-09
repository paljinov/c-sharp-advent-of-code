using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day13
{
    public class Maze
    {
        private const int START_X = 1;

        private const int START_Y = 1;

        public int CalculateFewestNumberOfStepsToReachCoordinates(
            int favoriteNumber,
            int destinationX,
            int destinationY
        )
        {
            Dictionary<(int x, int y), int> stepsToCoordinates = new Dictionary<(int x, int y), int>();
            int fewestNumberOfSteps = int.MaxValue;

            DoCalculateFewestNumberOfStepsToReachCoordinates(
                favoriteNumber,
                START_X,
                START_Y,
                destinationX,
                destinationY,
                stepsToCoordinates,
                0,
                ref fewestNumberOfSteps
            );

            return fewestNumberOfSteps;
        }

        public int CalculateDifferentLocationsVisitedAtMostSteps(int favoriteNumber, int mostSteps)
        {
            Dictionary<(int x, int y), int> stepsToCoordinates = new Dictionary<(int x, int y), int>();

            DoCalculateDifferentLocationsVisitedAtMostSteps(
                favoriteNumber,
                START_X,
                START_Y,
                stepsToCoordinates,
                0,
                mostSteps
            );

            return stepsToCoordinates.Count;
        }

        private void DoCalculateFewestNumberOfStepsToReachCoordinates(
            int favoriteNumber,
            int x,
            int y,
            int destinationX,
            int destinationY,
            Dictionary<(int x, int y), int> stepsToCoordinates,
            int steps,
            ref int minSteps
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

            if (stepsToCoordinates.ContainsKey((x, y)) && steps >= stepsToCoordinates[(x, y)])
            {
                return;
            }

            stepsToCoordinates[(x, y)] = steps;
            steps += 1;

            if (IsOpenSpace(favoriteNumber, x + 1, y))
            {
                DoCalculateFewestNumberOfStepsToReachCoordinates(
                    favoriteNumber,
                    x + 1,
                    y,
                    destinationX,
                    destinationY,
                    stepsToCoordinates,
                    steps,
                    ref minSteps
                );
            }

            if (IsOpenSpace(favoriteNumber, x, y + 1))
            {
                DoCalculateFewestNumberOfStepsToReachCoordinates(
                    favoriteNumber,
                    x,
                    y + 1,
                    destinationX,
                    destinationY,
                    stepsToCoordinates,
                    steps,
                    ref minSteps
                );
            }

            if (x > 0)
            {
                if (IsOpenSpace(favoriteNumber, x - 1, y))
                {
                    DoCalculateFewestNumberOfStepsToReachCoordinates(
                        favoriteNumber,
                        x - 1,
                        y,
                        destinationX,
                        destinationY,
                        stepsToCoordinates,
                        steps,
                        ref minSteps
                    );
                }
            }

            if (y > 0)
            {
                if (IsOpenSpace(favoriteNumber, x, y - 1))
                {
                    DoCalculateFewestNumberOfStepsToReachCoordinates(
                        favoriteNumber,
                        x,
                        y - 1,
                        destinationX,
                        destinationY,
                        stepsToCoordinates,
                        steps,
                        ref minSteps
                    );
                }
            }
        }

        private void DoCalculateDifferentLocationsVisitedAtMostSteps(
            int favoriteNumber,
            int x,
            int y,
            Dictionary<(int x, int y), int> stepsToCoordinates,
            int steps,
            int mostSteps
        )
        {
            if (steps > mostSteps)
            {
                return;
            }

            if (stepsToCoordinates.ContainsKey((x, y)) && steps >= stepsToCoordinates[(x, y)])
            {
                return;
            }

            stepsToCoordinates[(x, y)] = steps;
            steps += 1;

            if (IsOpenSpace(favoriteNumber, x + 1, y))
            {
                DoCalculateDifferentLocationsVisitedAtMostSteps(
                    favoriteNumber,
                    x + 1,
                    y,
                    stepsToCoordinates,
                    steps,
                    mostSteps
                );
            }

            if (IsOpenSpace(favoriteNumber, x, y + 1))
            {
                DoCalculateDifferentLocationsVisitedAtMostSteps(
                    favoriteNumber,
                    x,
                    y + 1,
                    stepsToCoordinates,
                    steps,
                    mostSteps
                );
            }

            if (x > 0)
            {
                if (IsOpenSpace(favoriteNumber, x - 1, y))
                {
                    DoCalculateDifferentLocationsVisitedAtMostSteps(
                        favoriteNumber,
                        x - 1,
                        y,
                        stepsToCoordinates,
                        steps,
                        mostSteps
                    );
                }
            }

            if (y > 0)
            {
                if (IsOpenSpace(favoriteNumber, x, y - 1))
                {
                    DoCalculateDifferentLocationsVisitedAtMostSteps(
                        favoriteNumber,
                        x,
                        y - 1,
                        stepsToCoordinates,
                        steps,
                        mostSteps
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
