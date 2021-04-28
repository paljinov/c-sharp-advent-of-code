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

            List<List<int>> asleepMinutesPerDay = new List<List<int>>();
            List<int> asleepMinutes = new List<int>();
            int day = 0;

            foreach ((DateTime fallsAsleep, DateTime wakesUp) in guardsMinutesAsleep[guardWhoHasMostMinutesAsleep])
            {
                // If day changed
                if (day != fallsAsleep.Day)
                {
                    day = fallsAsleep.Day;
                    asleepMinutes = new List<int>();
                }

                for (int t = fallsAsleep.Minute; t < wakesUp.Minute; t++)
                {
                    asleepMinutes.Add(t);
                }

                asleepMinutesPerDay.Add(asleepMinutes);
            }

            Dictionary<int, int> asleepInMinutes = new Dictionary<int, int>();
            for (int min = 0; min <= 59; min++)
            {
                asleepInMinutes[min] = 0;
            }

            foreach (List<int> minutes in asleepMinutesPerDay)
            {
                foreach (int minute in minutes)
                {
                    asleepInMinutes[minute]++;
                }
            }

            int mostTimesAsleepInMinute = 0;
            int mostTimesAsleep = 0;

            foreach (KeyValuePair<int, int> asleepInMinute in asleepInMinutes)
            {
                if (asleepInMinute.Value > mostTimesAsleep)
                {
                    mostTimesAsleep = asleepInMinute.Value;
                    mostTimesAsleepInMinute = asleepInMinute.Key;
                }
            }

            int product = guardWhoHasMostMinutesAsleep * mostTimesAsleepInMinute;

            return product;
        }

        public int CalculateGuardIdAndAsleepMinuteProductForSecondStrategy(Dictionary<DateTime, Record> guardsRecords)
        {
            guardsRecords = guardsRecords.OrderBy(gr => gr.Key).ToDictionary(gr => gr.Key, gr => gr.Value);
            Dictionary<int, List<(DateTime, DateTime)>> guardsMinutesAsleep = FindGuardsMinutesAsleep(guardsRecords);

            Dictionary<int, Dictionary<int, int>> guardsAsleepPerMinutes = new Dictionary<int, Dictionary<int, int>>();
            foreach (KeyValuePair<int, List<(DateTime, DateTime)>> guardMinutesAsleep in guardsMinutesAsleep)
            {
                foreach ((DateTime fallsAsleep, DateTime wakesUp) in guardMinutesAsleep.Value)
                {
                    for (int minute = fallsAsleep.Minute; minute < wakesUp.Minute; minute++)
                    {
                        if (!guardsAsleepPerMinutes.ContainsKey(guardMinutesAsleep.Key))
                        {
                            guardsAsleepPerMinutes[guardMinutesAsleep.Key] = new Dictionary<int, int>();
                            for (int min = 0; min <= 59; min++)
                            {
                                guardsAsleepPerMinutes[guardMinutesAsleep.Key][min] = 0;
                            }
                        }

                        guardsAsleepPerMinutes[guardMinutesAsleep.Key][minute]++;
                    }
                }
            }

            int guardId = 0;
            int mostFrequentlyAsleepMinute = 0;
            int mostFrequentlyAsleep = 0;

            foreach (KeyValuePair<int, Dictionary<int, int>> guardAsleepPerMinutes in guardsAsleepPerMinutes)
            {
                foreach (KeyValuePair<int, int> asleepPerMinute in guardAsleepPerMinutes.Value)
                {
                    if (asleepPerMinute.Value > mostFrequentlyAsleep)
                    {
                        guardId = guardAsleepPerMinutes.Key;
                        mostFrequentlyAsleepMinute = asleepPerMinute.Key;
                        mostFrequentlyAsleep = asleepPerMinute.Value;
                    }
                }
            }

            int product = guardId * mostFrequentlyAsleepMinute;

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
    }
}
