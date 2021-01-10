using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day14
{
    public class ReindeersFlightData
    {
        /// <summary>
        /// Flight duration in seconds.
        /// </summary>
        private readonly int flightDurationLimit = 2503;

        /// <summary>
        /// Calculate reindeers traveled distances after each second.
        /// </summary>
        /// <param name="reindeersFlightData"></param>
        /// <returns>
        /// Structure format:
        /// [
        ///     1st second => [
        ///         1st reindeer name => traveled distance,
        ///         2nd reindeer name => traveled distance,
        ///         ...
        ///     ],
        ///     2nd second => [],
        ///     ...
        /// ]
        /// </returns>
        public Dictionary<int, Dictionary<string, int>> CalculateReindeersTraveledDistancesAfterEachSecond(
            Dictionary<string, ReindeerFlight> reindeersFlightData
        )
        {
            Dictionary<int, Dictionary<string, int>> traveledDistancesAfterEachSecond =
                new Dictionary<int, Dictionary<string, int>>();
            // Initialize reindeers traveled distances after each second
            for (int sec = 1; sec <= flightDurationLimit; sec++)
            {
                traveledDistancesAfterEachSecond.Add(sec, new Dictionary<string, int>());
            }

            foreach (KeyValuePair<string, ReindeerFlight> reindeerFlightData in reindeersFlightData)
            {
                ReindeerFlight reindeerFlight = reindeerFlightData.Value;

                int traveledDistance = 0;
                int flightTime = reindeerFlight.FlightTime;
                int restTime = reindeerFlight.RestTime;

                for (int sec = 1; sec <= flightDurationLimit; sec++)
                {
                    // If there is still flight time left
                    if (flightTime > 0)
                    {
                        traveledDistance += reindeerFlight.FlightSpeed;
                        flightTime--;
                    }
                    // If there is still rest time left
                    else if (restTime > 0)
                    {
                        restTime--;
                    }

                    // If both flight and rest time are zero, the cycle repeats
                    if (flightTime == 0 && restTime == 0)
                    {
                        flightTime = reindeerFlight.FlightTime;
                        restTime = reindeerFlight.RestTime;
                    }

                    Dictionary<string, int> reindeersTraveledDistancesAfterSeconds =
                        traveledDistancesAfterEachSecond[sec];
                    reindeersTraveledDistancesAfterSeconds.Add(reindeerFlightData.Key, traveledDistance);
                    traveledDistancesAfterEachSecond[sec] = reindeersTraveledDistancesAfterSeconds;
                }
            }

            return traveledDistancesAfterEachSecond;
        }
    }
}
