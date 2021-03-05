using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day6
{
    public class Redistribute
    {
        public int CountRedistributionCycles(int[] memoryBanksBlocks)
        {
            (Dictionary<int, int[]> configurations, _) = GetMemoryBanksConfigurations(memoryBanksBlocks);

            return configurations.Count;
        }

        public int CountLoopSize(int[] memoryBanksBlocks)
        {
            int loopSize = 0;

            (Dictionary<int, int[]> configurations, int[] repeats) = GetMemoryBanksConfigurations(memoryBanksBlocks);
            foreach (KeyValuePair<int, int[]> configuration in configurations)
            {
                if (repeats.SequenceEqual(configuration.Value))
                {
                    loopSize = configurations.Count - configuration.Key;
                    break;
                }
            }

            return loopSize;
        }

        private (Dictionary<int, int[]> configurations, int[] repeats) GetMemoryBanksConfigurations(
            int[] memoryBanksBlocks
        )
        {
            Dictionary<int, int[]> configurations = new Dictionary<int, int[]>();

            while (!IsConfigurationAlreadySeen(memoryBanksBlocks, configurations))
            {
                configurations.Add(configurations.Count, memoryBanksBlocks.ToArray());

                int max = memoryBanksBlocks.Max();
                int maxIndex = Array.IndexOf(memoryBanksBlocks, max);

                int redistributionChunk = (int)Math.Floor((decimal)max / (memoryBanksBlocks.Length - 1));
                int redistributionTotal = redistributionChunk * (memoryBanksBlocks.Length - 1);
                if (redistributionChunk == 0)
                {
                    redistributionChunk = 1;
                    redistributionTotal = max;
                }

                memoryBanksBlocks[maxIndex] = max - redistributionTotal;
                int i = maxIndex + 1;
                while (redistributionTotal > 0)
                {
                    if (i == memoryBanksBlocks.Length)
                    {
                        i = 0;
                    }

                    if (i != maxIndex)
                    {
                        memoryBanksBlocks[i] += redistributionChunk;
                    }

                    redistributionTotal -= redistributionChunk;
                    i++;
                }
            }

            return (configurations, memoryBanksBlocks);
        }

        private bool IsConfigurationAlreadySeen(int[] memoryBanksBlocks, Dictionary<int, int[]> configurations)
        {
            foreach (KeyValuePair<int, int[]> configuration in configurations)
            {
                if (memoryBanksBlocks.SequenceEqual(configuration.Value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
