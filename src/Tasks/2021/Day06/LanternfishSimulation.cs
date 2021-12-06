using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day6
{
    public class LanternfishSimulation
    {
        private const int ZERO_INTERNAL_TIMER = 0;

        private const int CREATOR_LANTERNFISH_INTERNAL_TIMER = 6;

        private const int NEW_LANTERNFISH_INTERNAL_TIMER = 8;

        public long CountLanternfishAfterGivenDays(int[] lanternfishInternalTimers, int totalDays)
        {
            Dictionary<int, long> lanternfishInternalTimersOccurrences =
                GetLanternfishInternalTimersOccurrences(lanternfishInternalTimers);

            for (int day = 1; day <= totalDays; day++)
            {
                long zeroOccurrences = lanternfishInternalTimersOccurrences[ZERO_INTERNAL_TIMER];

                // Update lanternfish internal timers
                for (int i = 0; i < NEW_LANTERNFISH_INTERNAL_TIMER; i++)
                {
                    lanternfishInternalTimersOccurrences[i] = lanternfishInternalTimersOccurrences[i + 1];
                }

                // Lanternfish with internal timer equal to zero is creator which will reset and create new lanternfish
                lanternfishInternalTimersOccurrences[CREATOR_LANTERNFISH_INTERNAL_TIMER] += zeroOccurrences;
                lanternfishInternalTimersOccurrences[NEW_LANTERNFISH_INTERNAL_TIMER] = zeroOccurrences;
            }

            long totalLanternfish = lanternfishInternalTimersOccurrences.Values.Sum();

            return totalLanternfish;
        }

        private Dictionary<int, long> GetLanternfishInternalTimersOccurrences(int[] lanternfishInternalTimers)
        {
            Dictionary<int, long> lanternfishInternalTimersOccurrences = new Dictionary<int, long>();
            for (int i = 0; i <= NEW_LANTERNFISH_INTERNAL_TIMER; i++)
            {
                lanternfishInternalTimersOccurrences[i] = 0;
            }

            foreach (int lanternfishInternalTimer in lanternfishInternalTimers)
            {
                lanternfishInternalTimersOccurrences[lanternfishInternalTimer]++;
            }

            return lanternfishInternalTimersOccurrences;
        }
    }
}
