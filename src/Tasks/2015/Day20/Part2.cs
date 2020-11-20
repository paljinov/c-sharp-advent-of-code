/*
--- Part Two ---

The Elves decide they don't want to visit an infinite number of houses. Instead,
each Elf will stop after delivering presents to 50 houses. To make up for it,
they decide to deliver presents equal to eleven times their number at each
house.

With these changes, what is the new lowest house number of the house to get at
least as many presents as the number in your puzzle input?
*/

using System;

namespace App.Tasks.Year2015.Day20
{
    public class Part2 : ITask<int>
    {
        private const int PresentsPerElf = 11;

        public int Solution(string input)
        {
            int presents = int.Parse(input);

            // Maximum house number which will surely get at least as many presents as its number
            int maxHouseNumber = presents / PresentsPerElf;
            int lowestHouseNumber = maxHouseNumber;

            int[] houses = new int[maxHouseNumber + 1];
            Console.WriteLine(houses.Length - 1);

            // Iterating elves
            for (var i = 1; i <= maxHouseNumber; i++)
            {
                // Total visited houses by this elf
                int elfVisitedHouses = 0;

                // Iterating visited houses by elf
                for (var j = i; j <= maxHouseNumber; j += i)
                {
                    // Received presents for house
                    houses[j] += i * PresentsPerElf;

                    // If house received at least as many presents as its number
                    if (houses[j] >= presents)
                    {
                        lowestHouseNumber = Math.Min(j, lowestHouseNumber);
                    }

                    elfVisitedHouses++;
                    // Elf will stop after delivering presents to 50 houses
                    if (elfVisitedHouses == 50)
                    {
                        break;
                    }
                }
            }

            return lowestHouseNumber;
        }
    }
}
