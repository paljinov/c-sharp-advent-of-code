using System;
using System.Linq;

namespace App.Tasks.Year2018.Day13
{
    public class TracksMapRepository
    {
        public char[,] GetTracksMap(string input)
        {
            string[] tracksMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = tracksMapString.Length;
            int columns = tracksMapString.MaxBy(tm => tm.Length).Length;

            char[,] tracksMap = new char[rows, columns];

            for (int i = 0; i < tracksMapString.Length; i++)
            {
                for (int j = 0; j < tracksMapString[i].Length; j++)
                {
                    tracksMap[i, j] = tracksMapString[i][j];
                }
            }

            return tracksMap;
        }
    }
}
