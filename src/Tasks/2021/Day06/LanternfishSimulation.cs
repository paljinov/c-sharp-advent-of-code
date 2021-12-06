using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day6
{
    public class LanternfishSimulation
    {
        private const int CREATOR_LANTERNFISH_INTERNAL_TIMER = 6;

        private const int NEW_LANTERNFISH_INTERNAL_TIMER = 8;

        public int CountLanternfishAfterGivenDays(int[] lanternfishInternalTimers, int totalDays)
        {
            List<int> lanternfish = lanternfishInternalTimers.ToList();

            for (int day = 1; day <= totalDays; day++)
            {
                int lanternfishCount = lanternfish.Count;
                for (int i = 0; i < lanternfishCount; i++)
                {
                    lanternfish[i]--;
                    // Create new lanternfish
                    if (lanternfish[i] < 0)
                    {
                        lanternfish[i] = CREATOR_LANTERNFISH_INTERNAL_TIMER;
                        lanternfish.Add(NEW_LANTERNFISH_INTERNAL_TIMER);
                    }
                }
            }

            return lanternfish.Count;
        }
    }
}
