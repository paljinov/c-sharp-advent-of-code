using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day22
{
    public class MapRepository
    {
        private const char INFECTED_NODE = '#';

        public Dictionary<(int, int), bool> GetInfectedNodesMap(string input)
        {
            string[] mapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = mapString.Length;
            int columns = mapString[0].Length;

            Dictionary<(int, int), bool> infectedNodesMap = new Dictionary<(int, int), bool>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int y = rows - 1 - i - (int)(rows / 2);
                    int x = j - (int)(columns / 2);

                    if (mapString[i][j] == INFECTED_NODE)
                    {
                        infectedNodesMap[(x, y)] = true;
                    }
                    else
                    {
                        infectedNodesMap[(x, y)] = false;
                    }
                }
            }

            return infectedNodesMap;
        }
    }
}
