using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day10
{
    public class AdaptersChain
    {
        private const int CHARGING_OUTLET_JOLTAGE = 0;

        private const int MIN_JOLTAGE_DIFFERENCE = 1;

        private const int MAX_JOLTAGE_DIFFERENCE = 3;

        public int CalculateOneAndThreeJoltDifferencesProduct(int[] adapters)
        {
            int oneJoltDifferences = 0;
            // Your device's built-in adapter is always 3 higher than the highest adapter,
            // so that is one three jolt difference
            int threeJoltDifferences = 1;

            adapters = adapters.Concat(new int[] { CHARGING_OUTLET_JOLTAGE }).ToArray();
            Array.Sort(adapters);

            for (int i = 0; i < adapters.Length - 1; i++)
            {
                int joltageDifference = adapters[i + 1] - adapters[i];

                // If joltage difference is in given limits
                if (joltageDifference <= MAX_JOLTAGE_DIFFERENCE)
                {
                    if (joltageDifference == MIN_JOLTAGE_DIFFERENCE)
                    {
                        oneJoltDifferences++;
                    }
                    else if (joltageDifference == MAX_JOLTAGE_DIFFERENCE)
                    {
                        threeJoltDifferences++;
                    }
                }
                else
                {
                    break;
                }
            }

            int oneAndThreeJoltDifferencesProduct = oneJoltDifferences * threeJoltDifferences;

            return oneAndThreeJoltDifferencesProduct;
        }

        public long CountDistinctAdaptersArrangements(int[] adapters)
        {
            adapters = adapters.Concat(new int[] { CHARGING_OUTLET_JOLTAGE }).ToArray();
            Array.Sort(adapters);

            // Initializing different arrangments from adapter
            Dictionary<int, long> distinctArrangements = new Dictionary<int, long>
            {
                [adapters.Length - 1] = 1
            };

            for (int i = adapters.Length - 2; i >= 0; i--)
            {
                long arrangments = 0;

                // Only next 3 adapters can have allowed joltage difference
                int max = Math.Min(i + 1 + MAX_JOLTAGE_DIFFERENCE, adapters.Length);
                for (int j = i + 1; j < max; j++)
                {
                    // If joltage difference is in given limits
                    if (adapters[j] - adapters[i] <= MAX_JOLTAGE_DIFFERENCE)
                    {
                        arrangments += distinctArrangements[j];
                    }
                }

                distinctArrangements.Add(i, arrangments);
            }

            return distinctArrangements[0];
        }
    }
}
