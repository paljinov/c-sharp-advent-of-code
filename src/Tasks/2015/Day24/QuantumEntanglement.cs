using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day24
{
    public class QuantumEntanglement
    {
        public long GetFirstGroupQuantumEntanglement(int[] packageWeights, int groups)
        {
            long quantumEntanglement = long.MaxValue;

            // The packages need to be split into groups of exactly the same weight
            int groupWeight = packageWeights.Sum() / groups;

            for (int i = 1; i <= (int)Math.Floor((double)packageWeights.Length / 3); i++)
            {
                // If quantum entanglement is found all other first groups have bigger number of packages
                if (quantumEntanglement < long.MaxValue)
                {
                    break;
                }

                // Get package weights combinations of given length
                List<List<int>> packageWeightsGroups = GetPackageWeightsCombinations(packageWeights, i);
                foreach (List<int> packageWeightsGroup in packageWeightsGroups)
                {
                    // If group has appropriate weight
                    if (packageWeightsGroup.Sum() == groupWeight)
                    {
                        long product = CalculatePackageWeightsGroupProduct(packageWeightsGroup);
                        quantumEntanglement = Math.Min(quantumEntanglement, product);
                    }
                }
            }

            return quantumEntanglement;
        }


        private List<List<int>> GetPackageWeightsCombinations(int[] packageWeights, int groupLength)
        {
            List<List<int>> combinations = new List<List<int>>();

            if (packageWeights.Length == 0)
            {
                return combinations;
            }

            if (groupLength == 0)
            {
                combinations.Add(new List<int>());
                return combinations;
            }

            List<List<int>> subCombinations = GetPackageWeightsCombinations(
                packageWeights,
                groupLength - 1
            );

            foreach (List<int> subCombination in subCombinations)
            {
                // If sub-combination doesn't contain this package weight already
                if (!subCombination.Contains(packageWeights[0]))
                {
                    subCombination.Insert(0, packageWeights[0]);
                    combinations.Add(subCombination);
                }
            }

            combinations.AddRange(GetPackageWeightsCombinations(packageWeights[1..], groupLength));

            return combinations;
        }

        private long CalculatePackageWeightsGroupProduct(List<int> packageWeightsGroup)
        {
            long product = 1;
            foreach (int packageWeight in packageWeightsGroup)
            {
                product *= packageWeight;
            }

            return product;
        }
    }
}
