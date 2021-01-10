using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day14
{
    public class ReindeersDescriptionsRepository
    {
        public Dictionary<string, ReindeerDescription> GetReindeersDescriptions(string input)
        {
            Dictionary<string, ReindeerDescription> reindeersDescriptions =
                new Dictionary<string, ReindeerDescription>();

            Regex reindeerDescriptionRegex = new Regex(@"^(\w+).+?(\d+).+?(\d+).+?(\d+).+?$");

            string[] reindeersDescriptionsString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string reindeerDescriptionString in reindeersDescriptionsString)
            {
                Match match = reindeerDescriptionRegex.Match(reindeerDescriptionString);
                GroupCollection groups = match.Groups;

                reindeersDescriptions.Add(groups[1].Value, new ReindeerDescription
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
