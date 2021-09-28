using System;

namespace App.Tasks.Year2019.Day24
{
    public class AreaGridRepository
    {
        private const char BUG = '#';

        public bool[,] GetBugGrid(string input)
        {
            string[] bugGridString = input.Split(Environment.NewLine);

            int rows = bugGridString.Length;
            int columns = bugGridString[0].Length;

            bool[,] bugGrid = new bool[columns, rows];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (bugGridString[x][y] == BUG)
                    {
                        bugGrid[x, y] = true;
                    }
                }
            }

            return bugGrid;
        }
    }
}
