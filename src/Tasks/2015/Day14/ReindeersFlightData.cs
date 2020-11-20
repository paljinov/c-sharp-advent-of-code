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
        public const int FlightDurationLimit = 2503;

        /// <summary>
        /// Get reindeers flight data, key is reindeer name.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dictionary<string, ReindeerFlight> ParseInput(string input)
        {
            Dictionary<string, ReindeerFlight> reindeersFlightData = new Dictionary<string, ReindeerFlight>();

            Regex reindeerFlightDataRegex = new Regex(@"^(\w+).+?(\d+).+?(\d+).+?(\d+).+?$");

            string[] reindeersFlightDataString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string reindeerFlightDataString in reindeersFlightDataString)
            {
                Match match = reindeerFlightDataRegex.Match(reindeerFlightDataString);
                GroupCollection groups = match.Groups;

                reindeersFlightData.Add(groups[1].Value, new ReindeerFlight
                {
                    FlightSpeed = int.Parse(groups[2].Value),
                    FlightTime = int.Parse(groups[3].Value),
                    RestTime = int.Parse(groups[4].Value)
                });
            }

            return reindeersFlightData;
        }

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
            for (int sec = 1; sec <= FlightDurationLimit; sec++)
            {
                traveledDistancesAfterEachSecond.Add(sec, new Dictionary<string, int>());
            }

            foreach (KeyValuePair<string, ReindeerFlight> reindeerFlightData in reindeersFlightData)
            {
                ReindeerFlight reindeerFlight = reindeerFlightData.Value;

                int traveledDistance = 0;
                int flightTime = reindeerFlight.FlightTime;
                int restTime = reindeerFlight.RestTime;

                for (int sec = 1; sec <= FlightDurationLimit; sec++)
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
