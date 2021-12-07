using System;

namespace App.Tasks.Year2021.Day7
{
    public class CrabHorizontalPositionsRepository
    {
        public int[] GetCrabHorizontalPositions(string input)
        {
            string[] crabHorizontalPositionsString = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            int[] crabHorizontalPositions = new int[crabHorizontalPositionsString.Length];

            for (int i = 0; i < crabHorizontalPositionsString.Length; i++)
            {
                crabHorizontalPositions[i] = int.Parse(crabHorizontalPositionsString[i]);
            }

            return crabHorizontalPositions;
        }
    }
}
