using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day11
{
    public class FloorsArrangementRepository
    {
        public Dictionary<int, FloorObjects> GetObjectsArrangementByFloors(string input)
        {
            Dictionary<int, FloorObjects> floorsObjectsArrangement =
                new Dictionary<int, FloorObjects>();

            string[] floorsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex microchipRegex = new Regex(@"(\w+)\-compatible\smicrochip");
            Regex generatorRegex = new Regex(@"(\w+)\sgenerator");

            for (int floor = 1; floor <= floorsString.Length; floor++)
            {
                string floorString = floorsString[floor - 1];
                List<string> microchips = new List<string>();
                List<string> generators = new List<string>();

                MatchCollection microchipMatches = microchipRegex.Matches(floorString);
                foreach (Match microchipMatch in microchipMatches)
                {
                    microchips.Add(microchipMatch.Groups[1].Value);
                }

                MatchCollection generatorMatches = generatorRegex.Matches(floorString);
                foreach (Match generatorMatch in generatorMatches)
                {
                    generators.Add(generatorMatch.Groups[1].Value);
                }

                floorsObjectsArrangement.Add(floor, new FloorObjects
                {
                    Microchips = microchips,
                    Generators = generators
                });
            }

            return floorsObjectsArrangement;
        }
    }
}
