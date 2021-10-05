using System;
using System.Linq;

namespace App.Tasks.Year2019.Day20
{
    public class MazeMapRepository
    {
        public char[,] GetMazeMap(string input)
        {
            string[] mazeMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = mazeMapString.Length;
            int columns = mazeMapString.OrderByDescending(s => s.Length).First().Length;

            char[,] mazeMap = new char[rows, columns];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    var bla = mazeMapString[x];
                    if (y >= mazeMapString[x].Length)
                    {
                        mazeMap[x, y] = ' ';
                    }
                    else
                    {
                        mazeMap[x, y] = mazeMapString[x][y];
                    }
                }
            }

            return mazeMap;
        }
    }
}
