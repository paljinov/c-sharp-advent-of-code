using System.Collections.Generic;

namespace App.Tasks.Year2015.Day14
{
    public class ReindeersFlyingRace
    {
        /// <summary>
        /// Flight duration in seconds.
        /// </summary>
        private readonly int flightDurationLimit = 2503;

        /// <summary>
        /// Calculate reindeers traveled distances after each second.
        /// </summary>
        /// <param name="reindeersDescriptions"></param>
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
            Dictionary<string, ReindeerDescription> reindeersDescriptions
        )
        {
            Dictionary<int, Dictionary<string, int>> traveledDistancesAfterEachSecond =
                new Dictionary<int, Dictionary<string, int>>();
            // Initialize reindeers traveled distances after each second
            for (int sec = 1; sec <= flightDurationLimit; sec++)
            {
                traveledDistancesAfterEachSecond.Add(sec, new Dictionary<string, int>());
            }

            foreach (KeyValuePair<string, ReindeerDescription> reindeerDescription in reindeersDescriptions)
            {
                int traveledDistance = 0;
                int flightTime = reindeerDescription.Value.FlightTime;
                int restTime = reindeerDescription.Value.RestTime;

                for (int sec = 1; sec <= flightDurationLimit; sec++)
                {
                    // If there is still flight time left
                    if (flightTime > 0)
                    {
                        traveledDistance += reindeerDescription.Value.FlightSpeed;
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
                        flightTime = reindeerDescription.Value.FlightTime;
                        restTime = reindeerDescription.Value.RestTime;
                    }

                    Dictionary<string, int> reindeersTraveledDistancesAfterSeconds =
                        traveledDistancesAfterEachSecond[sec];
                    reindeersTraveledDistancesAfterSeconds.Add(reindeerDescription.Key, traveledDistance);
                    traveledDistancesAfterEachSecond[sec] = reindeersTraveledDistancesAfterSeconds;
                }
            }

            return traveledDistancesAfterEachSecond;
        }
    }
}
