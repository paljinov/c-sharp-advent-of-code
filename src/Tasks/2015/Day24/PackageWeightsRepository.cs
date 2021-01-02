using System;

namespace App.Tasks.Year2015.Day24
{
    public class PackageWeightsRepository
    {
        public int[] GetPackageWeights(string input)
        {
            string[] packageWeightsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[] packageWeights = new int[packageWeightsString.Length];

            for (int i = 0; i < packageWeightsString.Length; i++)
            {
                packageWeights[i] = int.Parse(packageWeightsString[i]);
            }

            return packageWeights;
        }
    }
}
