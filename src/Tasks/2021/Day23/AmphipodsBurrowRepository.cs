using System;

namespace App.Tasks.Year2021.Day23
{
    public class AmphipodsBurrowRepository
    {
        private const char OUTSIDE = ' ';

        private const char WALL = '#';

        public char[,] GetAmphipodsBurrow(string input)
        {
            string[] amphipodsBurrowString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = amphipodsBurrowString.Length;
            int columns = amphipodsBurrowString[0].Length;
            char[,] amphipodsBurrow = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j < amphipodsBurrowString[i].Length)
                    {
                        amphipodsBurrow[i, j] = amphipodsBurrowString[i][j];
                        if (amphipodsBurrow[i, j] == OUTSIDE)
                        {
                            amphipodsBurrow[i, j] = WALL;
                        }
                    }
                    else
                    {
                        amphipodsBurrow[i, j] = WALL;
                    }
                }
            }

            return amphipodsBurrow;
        }
    }
}
