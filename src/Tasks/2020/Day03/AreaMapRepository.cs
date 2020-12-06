using System;

namespace App.Tasks.Year2020.Day3
{
    public class AreaMapRepository
    {
        public bool[,] GetAreaMap(string input)
        {
            string[] areaMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // Initializing areaMap
            bool[,] areaMap = new bool[areaMapString.Length, areaMapString[0].Length];

            for (int i = 0; i < areaMapString.Length; i++)
            {
                string rowString = areaMapString[i];
                for (int j = 0; j < rowString.Length; j++)
                {
                    // If it is tree
                    if (rowString[j] == '#')
                    {
                        areaMap[i, j] = true;
                    }
                }
            }

            return areaMap;
        }
    }
}
