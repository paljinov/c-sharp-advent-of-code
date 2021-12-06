using System;

namespace App.Tasks.Year2021.Day6
{
    public class LanternfishInternalTimersRepository
    {
        public int[] GetLanternfishInternalTimers(string input)
        {
            string[] lanternfishInternalTimersString = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            int[] lanternfishInternalTimers = new int[lanternfishInternalTimersString.Length];

            for (int i = 0; i < lanternfishInternalTimersString.Length; i++)
            {
                lanternfishInternalTimers[i] = int.Parse(lanternfishInternalTimersString[i]);
            }

            return lanternfishInternalTimers;
        }
    }
}
