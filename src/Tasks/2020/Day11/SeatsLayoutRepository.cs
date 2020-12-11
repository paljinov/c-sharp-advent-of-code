using System;

namespace App.Tasks.Year2020.Day11
{
    public class SeatsLayoutRepository
    {
        public char[,] GetSeatsLayout(string input)
        {
            string[] seatsLayoutString = input.Split(Environment.NewLine);

            char[,] seatsLayout = new char[seatsLayoutString.Length, seatsLayoutString[0].Length];

            for (int i = 0; i < seatsLayoutString.Length; i++)
            {
                for (int j = 0; j < seatsLayoutString[i].Length; j++)
                {
                    seatsLayout[i, j] = seatsLayoutString[i][j];
                }
            }

            return seatsLayout;
        }
    }
}
