using System;
using System.Linq;

namespace App.Tasks.Year2018.Day13
{
    public class TracksMapRepository
    {
        private const char EMPTY_SPACE = ' ';

        public char[,] GetTracksMap(string input)
        {
            string[] tracksMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = tracksMapString.Length;
            int columns = tracksMapString.MaxBy(tm => tm.Length).Length;

            char[,] tracksMap = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    tracksMap[i, j] = EMPTY_SPACE;
                    if (j < tracksMapString[i].Length)
                    {
                        tracksMap[i, j] = tracksMapString[i][j];
                    }
                }
            }

            return tracksMap;
        }
    }
}
