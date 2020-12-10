using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace App.Tasks.Year2020.Day10
{
    public class AdaptersChain
    {
        private const int CHARGING_OUTLET_JOLTAGE = 0;

        public int CalculateOneAndJoltDifferencesProduct(int[] adapters)
        {
            Array.Sort(adapters);
            int highestAdapterJoltage = adapters[^1];

            int i;
            int adapterJoltage = CHARGING_OUTLET_JOLTAGE;
            int oneJoltDifferences = 0;
            int threeJoltDifferences = 0;

            while (adapterJoltage < highestAdapterJoltage)
            {
                if (adapters.Contains(adapterJoltage + 1))
                {
                    oneJoltDifferences++;
                    i = Array.IndexOf(adapters, adapterJoltage + 1);
                }
                else if (adapters.Contains(adapterJoltage + 2))
                {
                    i = Array.IndexOf(adapters, adapterJoltage + 2);
                }
                else if (adapters.Contains(adapterJoltage + 3))
                {
                    threeJoltDifferences++;
                    i = Array.IndexOf(adapters, adapterJoltage + 3);
                }
                else
                {
                    break;
                }

                adapterJoltage = adapters[i];
            }

            int product = oneJoltDifferences * (threeJoltDifferences + 1);

            return product;
        }

        public long CountDistinctAdaptersArrangements(int[] adapters)
        {
            adapters = adapters.Concat(new int[] { 0 }).ToArray();
            Array.Sort(adapters);

            Dictionary<int, long> cache = new Dictionary<int, long>
            {
                [adapters.Length - 1] = 1
            };

            for (var i = adapters.Length - 2; i >= 0; i--)
            {
                long chainsCount = 0;
                for (int chain = i + 1; chain < adapters.Length && adapters[chain] - adapters[i] <= 3; chain++)
                {
                    chainsCount += cache[chain];
                }
                cache[i] = chainsCount;
            }

            return cache[0];
        }
    }
}
