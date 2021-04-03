using System;

namespace App.Tasks.Year2018.Day1
{
    public class FrequencyChangesRepository
    {
        public int[] GetFrequencyChanges(string input)
        {
            string[] frequencyChangesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int[] frequencyChanges = new int[frequencyChangesString.Length];
            for (int i = 0; i < frequencyChangesString.Length; i++)
            {
                frequencyChanges[i] = int.Parse(frequencyChangesString[i]);
            }

            return frequencyChanges;
        }
    }
}
