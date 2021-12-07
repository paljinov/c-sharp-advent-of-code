using System;
using System.Linq;

namespace App.Tasks.Year2021.Day7
{
    public class CrabSubmarines
    {
        public int CalculateLeastSpentFuelWhichIsNeededToAlign(int[] crabHorizontalPositions)
        {
            int leastSpentFuel = DoCalculateLeastSpentFuelWhichIsNeededToAlign(crabHorizontalPositions, false);

            return leastSpentFuel;
        }

        public int CalculateLeastSpentFuelWhichIsNeededToAlignForIncreasingFuelCost(int[] crabHorizontalPositions)
        {
            int leastSpentFuel = DoCalculateLeastSpentFuelWhichIsNeededToAlign(crabHorizontalPositions, true);

            return leastSpentFuel;
        }

        private int DoCalculateLeastSpentFuelWhichIsNeededToAlign(
            int[] crabHorizontalPositions,
            bool increasingFuelCost
        )
        {
            int leastSpentFuel = int.MaxValue;

            int minHorizontalPosition = crabHorizontalPositions.Min();
            int maxHorizontalPosition = crabHorizontalPositions.Max();

            for (int hp = minHorizontalPosition; hp < maxHorizontalPosition; hp++)
            {
                int spentFuel = 0;
                foreach (int crabHorizontalPosition in crabHorizontalPositions)
                {
                    int steps = Math.Abs(crabHorizontalPosition - hp);
                    if (!increasingFuelCost)
                    {
                        spentFuel += steps;
                    }
                    else
                    {
                        spentFuel += steps * (steps + 1) / 2;
                    }

                }

                leastSpentFuel = Math.Min(leastSpentFuel, spentFuel);
            }

            return leastSpentFuel;
        }
    }
}
