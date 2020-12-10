using System;

namespace App.Tasks.Year2020.Day10
{
    public class AdaptersRepository
    {
        public int[] GetAdapters(string input)
        {
            string[] adaptersString = input.Split(Environment.NewLine);

            int[] adapters = new int[adaptersString.Length];

            for (int i = 0; i < adaptersString.Length; i++)
            {
                adapters[i] = int.Parse(adaptersString[i]);
            }

            return adapters;
        }
    }
}
