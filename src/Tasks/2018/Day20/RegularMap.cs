using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day20
{
    public class RegularMap
    {
        private const char BRANCH_START = '(';

        private const char BRANCH_END = ')';

        private const char BRANCH_OPTION = '|';

        private static readonly Dictionary<char, (int X, int Y)> directions = new Dictionary<char, (int X, int Y)>()
        {
            { 'N', (0, 1) },
            { 'S', (0, -1) },
            { 'E', (1, 0) },
            { 'W', (-1, 0) },
        };

        public int CalculateLargestNumberOfDoorsNeededToPassThroughToReachARoom(string regex)
        {
            Dictionary<(int, int), int> doors = GetDoorsOnPositions(regex);

            int largestNumberOfDoorsNeededToPassThroughToReachARoom = doors.Values.Max();
            return largestNumberOfDoorsNeededToPassThroughToReachARoom;
        }

        public int CountRoomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors(
            string regex,
            int minDoors
        )
        {
            Dictionary<(int, int), int> doors = GetDoorsOnPositions(regex);

            int roomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastDoors =
                doors.Count(d => d.Value >= minDoors);

            return roomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastDoors;
        }

        public Dictionary<(int, int), int> GetDoorsOnPositions(string regex)
        {
            Stack<(int X, int Y)> path = new Stack<(int X, int Y)>();
            Dictionary<(int, int), int> doors = new Dictionary<(int, int), int>();

            int x = 0;
            int y = 0;
            int previousX = 0;
            int previousY = 0;

            for (int i = 1; i < regex.Length - 1; i++)
            {
                char character = regex[i];

                if (character == BRANCH_START)
                {
                    path.Push((x, y));
                }
                else if (character == BRANCH_END)
                {
                    (x, y) = path.Pop();
                }
                else if (character == BRANCH_OPTION)
                {
                    (x, y) = path.Peek();
                }
                // Make step
                else
                {
                    x += directions[character].X;
                    y += directions[character].Y;

                    if (!doors.ContainsKey((previousX, previousY)))
                    {
                        doors[(previousX, previousY)] = 0;
                    }

                    if (doors.ContainsKey((x, y)))
                    {
                        doors[(x, y)] = Math.Min(doors[(x, y)], doors[(previousX, previousY)] + 1);
                    }
                    else
                    {
                        doors[(x, y)] = doors[(previousX, previousY)] + 1;
                    }
                }

                (previousX, previousY) = (x, y);
            }

            return doors;
        }
    }
}
