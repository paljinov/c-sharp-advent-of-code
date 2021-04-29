using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day4
{
    public class Guards
    {
        public int CalculateGuardIdAndAsleepMinuteProductForFirstStrategy(Dictionary<DateTime, Record> guardsRecords)
        {
            guardsRecords = guardsRecords.OrderBy(gr => gr.Key).ToDictionary(gr => gr.Key, gr => gr.Value);
            Dictionary<int, List<(DateTime, DateTime)>> guardsMinutesAsleep = FindGuardsMinutesAsleep(guardsRecords);

            int guardWhoHasMostMinutesAsleep = FindGuardWhoHasMostMinutesAsleep(guardsMinutesAsleep);
            Dictionary<int, int> guardDaysAsleepForEachMinute =
                FindGuardDaysAsleepForEachMinute(guardsMinutesAsleep[guardWhoHasMostMinutesAsleep]);

            int minuteWithMostDaysAsleep = FindMinuteWithMostDaysAsleepForGuard(guardDaysAsleepForEachMinute);

            int product = guardWhoHasMostMinutesAsleep * minuteWithMostDaysAsleep;

            return product;
        }

        public int CalculateGuardIdAndAsleepMinuteProductForSecondStrategy(Dictionary<DateTime, Record> guardsRecords)
        {
            guardsRecords = guardsRecords.OrderBy(gr => gr.Key).ToDictionary(gr => gr.Key, gr => gr.Value);
            Dictionary<int, List<(DateTime, DateTime)>> guardsMinutesAsleep = FindGuardsMinutesAsleep(guardsRecords);

            Dictionary<int, Dictionary<int, int>> guardsDaysAsleepForEachMinute =
                new Dictionary<int, Dictionary<int, int>>();
            foreach (int guardId in guardsMinutesAsleep.Keys)
            {
                guardsDaysAsleepForEachMinute[guardId] = FindGuardDaysAsleepForEachMinute(guardsMinutesAsleep[guardId]);
            }

            int guardWhoIsMostFrequentlyAsleepOnSameMinute = guardsDaysAsleepForEachMinute.First().Key;
            int mostFrequentlyAsleepMinute = FindMinuteWithMostDaysAsleepForGuard(
                guardsDaysAsleepForEachMinute[guardWhoIsMostFrequentlyAsleepOnSameMinute]);
            int totalDaysForMostFrequentlyAsleepMinute = 0;

            foreach (KeyValuePair<int, Dictionary<int, int>> guardDaysAsleepForEachMinute in guardsDaysAsleepForEachMinute)
            {
                if (guardDaysAsleepForEachMinute.Value.Count > 0)
                {
                    int minuteWithMostDaysAsleep =
                        FindMinuteWithMostDaysAsleepForGuard(guardDaysAsleepForEachMinute.Value);
                    int daysAsleep = guardDaysAsleepForEachMinute.Value[minuteWithMostDaysAsleep];

                    if (daysAsleep > totalDaysForMostFrequentlyAsleepMinute)
                    {
                        guardWhoIsMostFrequentlyAsleepOnSameMinute = guardDaysAsleepForEachMinute.Key;
                        mostFrequentlyAsleepMinute = minuteWithMostDaysAsleep;
                        totalDaysForMostFrequentlyAsleepMinute = daysAsleep;
                    }
                }
            }

            int product = guardWhoIsMostFrequentlyAsleepOnSameMinute * mostFrequentlyAsleepMinute;

            return product;
        }

        private Dictionary<int, List<(DateTime, DateTime)>> FindGuardsMinutesAsleep(
            Dictionary<DateTime, Record> guardsRecords
        )
        {
            Dictionary<int, List<(DateTime, DateTime)>> guardsMinutesAsleep =
                new Dictionary<int, List<(DateTime, DateTime)>>();

            int guardId = 0;
            DateTime asleep = guardsRecords.First().Key;
            foreach (KeyValuePair<DateTime, Record> record in guardsRecords)
            {
                switch (record.Value.Action)
                {
                    case Action.BeginsShift:
                        guardId = record.Value.GuardId.Value;
                        if (!guardsMinutesAsleep.ContainsKey(guardId))
                        {
                            guardsMinutesAsleep[guardId] = new List<(DateTime, DateTime)>();
                        }
                        break;
                    case Action.FallsAsleep:
                        asleep = record.Key;
                        break;
                    case Action.WakesUp:
                        guardsMinutesAsleep[guardId].Add((asleep, record.Key));
                        break;
                }
            }

            return guardsMinutesAsleep;
        }

        private int FindGuardWhoHasMostMinutesAsleep(Dictionary<int, List<(DateTime, DateTime)>> guardsMinutesAsleep)
        {
            int guardWhoHasMostMinutesAsleep = guardsMinutesAsleep.First().Key;
            int mostMinutesAsleep = 0;

            foreach (KeyValuePair<int, List<(DateTime, DateTime)>> guardMinutesAsleep in guardsMinutesAsleep)
            {
                int minutesAsleep = 0;
                foreach ((DateTime fallsAsleep, DateTime wakesUp) in guardMinutesAsleep.Value)
                {
                    minutesAsleep += wakesUp.Minute - fallsAsleep.Minute;
                }

                if (minutesAsleep > mostMinutesAsleep)
                {
                    mostMinutesAsleep = minutesAsleep;
                    guardWhoHasMostMinutesAsleep = guardMinutesAsleep.Key;
                }
            }

            return guardWhoHasMostMinutesAsleep;
        }

        private Dictionary<int, int> FindGuardDaysAsleepForEachMinute(List<(DateTime, DateTime)> guardMinutesAsleep)
        {
            Dictionary<int, int> daysAsleepForEachMinute = new Dictionary<int, int>();
            foreach ((DateTime fallsAsleep, DateTime wakesUp) in guardMinutesAsleep)
            {
                for (int minute = fallsAsleep.Minute; minute < wakesUp.Minute; minute++)
                {
                    if (!daysAsleepForEachMinute.ContainsKey(minute))
                    {
                        daysAsleepForEachMinute[minute] = 1;
                    }
                    else
                    {
                        daysAsleepForEachMinute[minute]++;
                    }
                }
            }

            return daysAsleepForEachMinute;
        }

        private int FindMinuteWithMostDaysAsleepForGuard(Dictionary<int, int> guardDaysAsleepForEachMinute)
        {
            int minuteWithMostDaysAsleep = 0;
            foreach (KeyValuePair<int, int> guardDaysAsleepForMinute in guardDaysAsleepForEachMinute)
            {
                if (!guardDaysAsleepForEachMinute.ContainsKey(minuteWithMostDaysAsleep)
                    || guardDaysAsleepForMinute.Value > guardDaysAsleepForEachMinute[minuteWithMostDaysAsleep])
                {
                    minuteWithMostDaysAsleep = guardDaysAsleepForMinute.Key;
                }
            }

            return minuteWithMostDaysAsleep;
        }
    }
}
