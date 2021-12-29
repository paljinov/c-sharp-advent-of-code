using System;

namespace App.Tasks.Year2021.Day23
{
    public class AmphipodsBurrowRepository
    {
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
                    amphipodsBurrow[i, j] = amphipodsBurrowString[i][j];
                }
            }

            return amphipodsBurrow;
        }
    }
}
