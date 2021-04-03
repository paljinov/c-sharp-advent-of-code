using System.Linq;

namespace App.Tasks.Year2018.Day1
{
    public class Frequency
    {
        public int ResultingFrequencyAfterAllChanges(int[] frequencyChanges)
        {
            int resultingFrequency = frequencyChanges.Sum();
            return resultingFrequency;
        }
    }
}
