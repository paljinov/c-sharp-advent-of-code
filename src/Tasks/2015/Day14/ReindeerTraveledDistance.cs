using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day14
{
    class ReindeerTraveledDistance
    {
        public static List<Reindeer> ParseReindeerDescriptions(string input)
        {
            List<Reindeer> reindeerDescriptions = new List<Reindeer>();

            Regex reindeerDescriptionRegex = new Regex(@"^.+?(\d+).+?(\d+).+?(\d+).+$");

            string[] reindeerDescriptionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string reindeerDescriptionString in reindeerDescriptionsString)
            {
                Match match = reindeerDescriptionRegex.Match(reindeerDescriptionString);
                GroupCollection groups = match.Groups;

                reindeerDescriptions.Add(new Reindeer
                {
                    FlightSpeed = int.Parse(groups[1].Value),
                    FlightTime = int.Parse(groups[2].Value),
                    RestTime = int.Parse(groups[3].Value)
                });
            }

            return reindeerDescriptions;
        }
    }
}
