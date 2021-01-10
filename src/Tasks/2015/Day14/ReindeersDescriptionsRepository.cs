using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day14
{
    public class ReindeersDescriptionsRepository
    {
        public Dictionary<string, ReindeerFlight> GetReindeersDescriptions(string input)
        {
            Dictionary<string, ReindeerFlight> reindeersDescriptions = new Dictionary<string, ReindeerFlight>();

            Regex reindeerDescriptionRegex = new Regex(@"^(\w+).+?(\d+).+?(\d+).+?(\d+).+?$");

            string[] reindeersDescriptionsString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string reindeerDescriptionString in reindeersDescriptionsString)
            {
                Match match = reindeerDescriptionRegex.Match(reindeerDescriptionString);
                GroupCollection groups = match.Groups;

                reindeersDescriptions.Add(groups[1].Value, new ReindeerFlight
                {
                    FlightSpeed = int.Parse(groups[2].Value),
                    FlightTime = int.Parse(groups[3].Value),
                    RestTime = int.Parse(groups[4].Value)
                });
            }

            return reindeersDescriptions;
        }
    }
}
