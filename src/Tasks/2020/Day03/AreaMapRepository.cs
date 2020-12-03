using System;
using System.Linq;

namespace App.Tasks.Year2020.Day3
{
    public class AreaMapRepository
    {
        public bool[,] GetAreaMap(string input, int right, int down)
        {
            string[] areaMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int rows = areaMapString.Length;
            int columns = areaMapString[0].Length;

            int rowRepetitions = (int)Math.Ceiling((double)rows / (double)down) * right;

            // Initializing areaMap with sufficient number of coordinates
            bool[,] areaMap = new bool[rows, columns * rowRepetitions];

            for (int i = 0; i < rows; i++)
            {
                string rowString = areaMapString[i];
                string rowStringAfterRepetitions = string.Concat(Enumerable.Repeat(rowString, rowRepetitions));

                for (int j = 0; j < rowStringAfterRepetitions.Length; j++)
                {
                    // If it is tree
                    if (rowStringAfterRepetitions[j] == '#')
                    {
                        areaMap[i, j] = true;
                    }
                }
            }

            return areaMap;
        }
    }
}
