using System;

namespace App.Tasks.Year2015.Day20
{
    public class HouseNumber
    {
        public int CalculateLowestHouseNumberWhichGetsAtLeastPresents(int atLeastPresents, int presentsPerElf)
        {
            // Maximum house number which will surely get at least as many presents as its number
            int maxHouseNumber = atLeastPresents / presentsPerElf;
            int lowestHouseNumber = maxHouseNumber;

            int[] houses = new int[maxHouseNumber + 1];

            // Iterating elves
            for (var i = 1; i <= maxHouseNumber; i++)
            {
                // Iterating visited houses by elf
                for (var j = i; j <= maxHouseNumber; j += i)
                {
                    // Received presents for house
                    houses[j] += i * presentsPerElf;

                    // If house received at least as many presents as its number
                    if (houses[j] >= atLeastPresents)
                    {
                        lowestHouseNumber = Math.Min(j, lowestHouseNumber);
                    }
                }
            }

            return lowestHouseNumber;
        }

        public int CalculateLowestHouseNumberWhichGetsAtLeastPresentsWhenElfStopsAtFiftyHouses(
            int atLeastPresents,
            int presentsPerElf
        )
        {
            // Maximum house number which will surely get at least as many presents as its number
            int maxHouseNumber = atLeastPresents / presentsPerElf;
            int lowestHouseNumber = maxHouseNumber;

            int[] houses = new int[maxHouseNumber + 1];

            // Iterating elves
            for (var i = 1; i <= maxHouseNumber; i++)
            {
                // Total visited houses by this elf
                int elfVisitedHouses = 0;

                // Iterating visited houses by elf
                for (var j = i; j <= maxHouseNumber; j += i)
                {
                    // Received presents for house
                    houses[j] += i * presentsPerElf;

                    // If house received at least as many presents as its number
                    if (houses[j] >= atLeastPresents)
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
