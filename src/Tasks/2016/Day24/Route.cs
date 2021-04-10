using System;
using System.Collections.Generic;

namespace App.Tasks.Year2016.Day24
{
    public class Route
    {
        private const char WALL = '#';
        private const char PASSAGE = '.';
        private const int ZERO = 0;

        public int CalculateFewestNumberOfStepsRequiredToVisitEveryNonZeroNumber(char[,] map, bool returnToZero = false)
        {
            int fewestNumberOfSteps = int.MaxValue;

            SortedDictionary<int, (int, int)> numbers = GetNumbers(map);
            Dictionary<(int, int), int> numbersDistances = CalculateNumbersDistances(map, numbers);

            DoCalculateFewestNumberOfStepsRequiredToVisitEveryNonZeroNumber(
                numbersDistances,
                ZERO,
                numbers.Count,
                new HashSet<int>() { ZERO },
                0,
                ref fewestNumberOfSteps,
                returnToZero
            );

            return fewestNumberOfSteps;
        }

        private SortedDictionary<int, (int, int)> GetNumbers(char[,] map)
        {
            SortedDictionary<int, (int, int)> numbers = new SortedDictionary<int, (int, int)>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    // If number
                    if (map[i, j] != WALL && map[i, j] != PASSAGE)
                    {
                        numbers.Add((int)char.GetNumericValue(map[i, j]), (i, j));
                    }
                }
            }

            return numbers;
        }

        private Dictionary<(int, int), int> CalculateNumbersDistances(
            char[,] map,
            SortedDictionary<int, (int, int)> numbers
        )
        {
            Dictionary<(int, int), int> numbersDistances = new Dictionary<(int, int), int>();

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    int distance = int.MaxValue;
                    CalculateCoordinatesDistance(
                        map,
                        numbers[i],
                        numbers[j],
                        0,
                        new Dictionary<(int, int), int>(),
                        ref distance
                    );

                    numbersDistances.Add((i, j), distance);
                }
            }

            return numbersDistances;
        }

        private void CalculateCoordinatesDistance(
            char[,] map,
            (int x, int y) start,
            (int x, int y) end,
            int steps,
            Dictionary<(int, int), int> visitedCoordinates,
            ref int distance)
        {
            // If this coordinate was already visited in equal number or less steps
            if (visitedCoordinates.ContainsKey((start.x, start.y)) && visitedCoordinates[(start.x, start.y)] <= steps)
            {
                return;
            }

            // If this cannot be shortest distance
            if (distance <= steps)
            {
                return;
            }

            // If end number is reached
            if (start.x == end.x && start.y == end.y)
            {
                distance = steps;
                return;
            }

            visitedCoordinates[(start.x, start.y)] = steps;
            steps++;

            // Left
            if (map[start.x - 1, start.y] != WALL)
            {
                CalculateCoordinatesDistance(map, (start.x - 1, start.y), end, steps, visitedCoordinates, ref distance);
            }

            // Right
            if (map[start.x + 1, start.y] != WALL)
            {
                CalculateCoordinatesDistance(map, (start.x + 1, start.y), end, steps, visitedCoordinates, ref distance);
            }

            // Down
            if (map[start.x, start.y - 1] != WALL)
            {
                CalculateCoordinatesDistance(map, (start.x, start.y - 1), end, steps, visitedCoordinates, ref distance);
            }

            // Top
            if (map[start.x, start.y + 1] != WALL)
            {
                CalculateCoordinatesDistance(map, (start.x, start.y + 1), end, steps, visitedCoordinates, ref distance);
            }
        }

        private void DoCalculateFewestNumberOfStepsRequiredToVisitEveryNonZeroNumber(
            Dictionary<(int, int), int> numbersDistances,
            int currentNumber,
            int totalNumbers,
            HashSet<int> visitedNumbers,
            int steps,
            ref int fewestNumberOfSteps,
            bool returnToZero
        )
        {
            foreach (KeyValuePair<(int, int), int> distance in numbersDistances)
            {
                // Distances for current number
                if (distance.Key.Item1 == currentNumber || distance.Key.Item2 == currentNumber)
                {
                    int updatedCurrentNumber =
                        distance.Key.Item1 == currentNumber ? distance.Key.Item2 : distance.Key.Item1;

                    // If number is not already visited
                    if (!visitedNumbers.Contains(updatedCurrentNumber))
                    {
                        HashSet<int> updatedVisitedNumbers = new HashSet<int>(visitedNumbers)
                        {
                            updatedCurrentNumber
                        };

                        // If all numbers are visited
                        if (updatedVisitedNumbers.Count == totalNumbers)
                        {
                            if (returnToZero)
                            {
                                updatedVisitedNumbers.Remove(ZERO);
                                returnToZero = false;

                                DoCalculateFewestNumberOfStepsRequiredToVisitEveryNonZeroNumber(
                                    numbersDistances,
                                    updatedCurrentNumber,
                                    totalNumbers,
                                    updatedVisitedNumbers,
                                    steps + distance.Value,
                                    ref fewestNumberOfSteps,
                                    returnToZero
                                );
                            }
                            else
                            {
                                fewestNumberOfSteps = Math.Min(steps + distance.Value, fewestNumberOfSteps);
                            }
                        }
                        else
                        {
                            DoCalculateFewestNumberOfStepsRequiredToVisitEveryNonZeroNumber(
                                numbersDistances,
                                updatedCurrentNumber,
                                totalNumbers,
                                updatedVisitedNumbers,
                                steps + distance.Value,
                                ref fewestNumberOfSteps,
                                returnToZero
                            );
                        }
                    }
                }
            }
        }
    }
}
