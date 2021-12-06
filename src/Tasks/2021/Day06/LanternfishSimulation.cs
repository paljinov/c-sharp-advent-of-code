using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day6
{
    public class LanternfishSimulation
    {
        private const int CREATOR_LANTERNFISH_INTERNAL_TIMER = 6;

        private const int NEW_LANTERNFISH_INTERNAL_TIMER = 8;

        public long CountLanternfishAfterGivenDays(int[] lanternfishInternalTimers, int totalDays)
        {
            Dictionary<int, long> lanternfish = GetLanternfishInternalTimersOccurrences(lanternfishInternalTimers);

            for (int day = 1; day <= totalDays; day++)
            {
                long zeroInternalTimerOccurrences = lanternfish[0];

                for (int i = 0; i < NEW_LANTERNFISH_INTERNAL_TIMER; i++)
                {
                    lanternfish[i] = lanternfish[i + 1];
                }

                lanternfish[CREATOR_LANTERNFISH_INTERNAL_TIMER] += zeroInternalTimerOccurrences;
                lanternfish[NEW_LANTERNFISH_INTERNAL_TIMER] = zeroInternalTimerOccurrences;
            }

            long totalLanternfish = lanternfish.Values.Sum();

            return totalLanternfish;
        }

        private Dictionary<int, long> GetLanternfishInternalTimersOccurrences(int[] lanternfishInternalTimers)
        {
            Dictionary<int, long> lanternfish = new Dictionary<int, long>();
            for (int i = 0; i <= NEW_LANTERNFISH_INTERNAL_TIMER; i++)
            {
                lanternfish[i] = 0;
            }

            foreach (int lanternfishInternalTimer in lanternfishInternalTimers)
            {
                lanternfish[lanternfishInternalTimer]++;
            }

            return lanternfish;
        }
    }
}
