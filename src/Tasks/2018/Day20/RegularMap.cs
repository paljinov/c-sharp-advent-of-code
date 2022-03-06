using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day20
{
    public class RegularMap
    {
        private static readonly Dictionary<char, (int X, int Y)> directions = new Dictionary<char, (int X, int Y)>()
        {
            { 'N', (0, 1) },
            { 'S', (0, -1) },
            { 'E', (1, 0) },
            { 'W', (-1, 0) },
        };

        public int CalculateLargestNumberOfDoorsNeededToPassThroughToReachRoom(string regex)
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

                // Start branch
                if (character == '(')
                {
                    path.Push((x, y));
                }
                // End branch
                else if (character == ')')
                {
                    (x, y) = path.Pop();
                }
                // Door
                else if (character == '|')
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

            int largestNumberOfDoorsNeededToPassThroughToReachRoom = doors.Values.Max();

            return largestNumberOfDoorsNeededToPassThroughToReachRoom;
        }
    }
}
