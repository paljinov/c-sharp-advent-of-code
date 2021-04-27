using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day4
{
    public class Guards
    {
        public int FindProductOfIdAndMinuteForGuardWhoIsMostLikelyToBeAsleepAtSpecificMinute(
            Dictionary<DateTime, Record> guardsRecords)
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
            for (int min = 1; min <= 60; min++)
            {
                asleepInMinutes[min] = 0;
            }

            foreach (List<int> da in asleepMinutesPerDay)
            {
                foreach (int minute in da)
                {
                    asleepInMinutes[minute]++;
                }
            }

            int mostTimesAsleepInMinute = 1;
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
