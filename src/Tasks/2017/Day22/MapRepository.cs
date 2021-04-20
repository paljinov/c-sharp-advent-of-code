using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day22
{
    public class MapRepository
    {
        public Dictionary<(int, int), char> GetNodesMap(string input)
        {
            string[] mapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = mapString.Length;
            int columns = mapString[0].Length;

            Dictionary<(int, int), char> nodesMap = new Dictionary<(int, int), char>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int y = rows - 1 - i - (int)(rows / 2);
                    int x = j - (int)(columns / 2);

                    nodesMap[(x, y)] = mapString[i][j];
                }
            }

            return nodesMap;
        }
    }
}
