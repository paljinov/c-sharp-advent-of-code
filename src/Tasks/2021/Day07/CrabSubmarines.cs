using System;
using System.Linq;

namespace App.Tasks.Year2021.Day7
{
    public class CrabSubmarines
    {
        public int CalculateLeastSpentFuelWhichIsNeededToAlign(int[] crabHorizontalPositions)
        {
            int leastSpentFuel = int.MaxValue;

            int minHorizontalPosition = crabHorizontalPositions.Min();
            int maxHorizontalPosition = crabHorizontalPositions.Max();

            for (int hp = minHorizontalPosition; hp < maxHorizontalPosition; hp++)
            {
                int spentFuel = 0;
                foreach (int crabHorizontalPosition in crabHorizontalPositions)
                {
                    spentFuel += Math.Abs(crabHorizontalPosition - hp);
                }

                leastSpentFuel = Math.Min(leastSpentFuel, spentFuel);
            }

            return leastSpentFuel;
        }
    }
}
