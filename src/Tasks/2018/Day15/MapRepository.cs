using System;

namespace App.Tasks.Year2018.Day15
{
    public class MapRepository
    {
        public char[,] GetMap(string input)
        {
            string[] mapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = mapString.Length;
            int columns = mapString[0].Length;
            char[,] map = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    map[i, j] = mapString[i][j];
                }
            }

            return map;
        }
    }
}
