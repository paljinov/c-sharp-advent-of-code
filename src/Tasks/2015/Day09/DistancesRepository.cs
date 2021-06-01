using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day9
{
    public class DistancesRepository
    {
        public LocationsDistance[] GetDistances(string input)
        {
            string[] distancesArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            LocationsDistance[] distances = new LocationsDistance[distancesArray.Length];

            Regex distanceRegex = new Regex(@"^(\w+)\sto\s(\w+)\s\=\s(\d+)$");

            for (int i = 0; i < distancesArray.Length; i++)
            {
                Match distanceMatches = distanceRegex.Match(distancesArray[i]);
                GroupCollection distanceGroups = distanceMatches.Groups;

                distances[i] = new LocationsDistance
                {
                    StartLocation = distanceGroups[1].Value,
                    EndLocation = distanceGroups[2].Value,
                    Distance = int.Parse(distanceGroups[3].Value)
                };
            }

            return distances;
        }
    }
}
