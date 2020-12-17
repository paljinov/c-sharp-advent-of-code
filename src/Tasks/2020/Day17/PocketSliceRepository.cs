using System;

namespace App.Tasks.Year2020.Day17
{
    public class PocketSliceRepository
    {
        private const char ACTIVE_CUBE = '#';

        public bool[,] GetInitialPocket2DimensionalSlice(string input)
        {
            string[] initialPocketSliceString = input.Split(Environment.NewLine);

            bool[,] initialPocketSlice = new bool[initialPocketSliceString.Length, initialPocketSliceString[0].Length];

            for (int x = 0; x < initialPocketSliceString.Length; x++)
            {
                for (int y = 0; y < initialPocketSliceString[x].Length; y++)
                {
                    if (initialPocketSliceString[x][y] == ACTIVE_CUBE)
                    {
                        initialPocketSlice[x, y] = true;
                    }
                }
            }

            return initialPocketSlice;
        }
    }
}
