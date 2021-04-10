using System;

namespace App.Tasks.Year2016.Day24
{
    public class MapRepository
    {
        public char[,] GetMap(string input)
        {
            string[] mapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = mapString.Length;
            int columns = mapString[0].Length;
            char[,] map = new char[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    map[j, i] = mapString[i][j];
                }
            }

            return map;
        }
    }
}
