using System;
using System.Collections.Generic;

namespace App.Tasks.Year2020.Day3
{
    public class AreaMapRepository
    {
        public List<int[]> GetAreaMap(string input, int right, int down)
        {
            List<int[]> areaMap = new List<int[]>();

            string[] areaMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int rowRepetitions = (int)Math.Ceiling((double)(areaMapString.Length / down)) * right;

            foreach (string rowString in areaMapString)
            {
                int rowLength = rowString.Length;

                int[] row = new int[rowLength * rowRepetitions];

                for (int i = 0; i < rowRepetitions; i++)
                {
                    for (int j = 0; j < rowLength; j++)
                    {
                        char character = rowString[j];

                        // If it is tree
                        if (character == '#')
                        {
                            row[i * rowLength + j] = 1;
                        }
                    }
                }

                areaMap.Add(row);
            }

            return areaMap;
        }
    }
}
