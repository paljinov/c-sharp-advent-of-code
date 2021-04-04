using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day1
{
    public class Frequency
    {
        public int CalculateResultingFrequencyAfterAllChanges(int[] frequencyChanges)
        {
            int resultingFrequency = frequencyChanges.Sum();
            return resultingFrequency;
        }

        public int CalculateFirstFrequencyDeviceReachesTwice(int[] frequencyChanges)
        {
            int frequency = 0;

            Dictionary<int, int> frequencies = new Dictionary<int, int> {
                { frequency, 1 }
            };
            bool firstFrequencyReachedTwice = false;

            while (!firstFrequencyReachedTwice)
            {
                foreach (int frequencyChange in frequencyChanges)
                {
                    frequency += frequencyChange;
                    if (!frequencies.ContainsKey(frequency))
                    {
                        frequencies[frequency] = 1;
                    }
                    else
                    {
                        frequencies[frequency] += 1;
                        firstFrequencyReachedTwice = true;
                        break;
                    }
                }
            }

            return frequency;
        }
    }
}
