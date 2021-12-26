using System;

namespace App.Tasks.Year2021.Day25
{
    public class SeaCucumbersMapRepository
    {
        public char[,] GetSeaCucumbersMap(string input)
        {
            string[] seaCucumbersMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = seaCucumbersMapString.Length;
            int columns = seaCucumbersMapString[0].Length;
            char[,] seaCucumbersMap = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    seaCucumbersMap[i, j] = seaCucumbersMapString[i][j];
                }
            }

            return seaCucumbersMap;
        }
    }
}
