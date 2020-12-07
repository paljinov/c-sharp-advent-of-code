using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day7
{
    public class BagsRepository
    {
        public Dictionary<string, Dictionary<string, int>> GetBags(string input)
        {
            Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();

            string[] bagsString = input.Split(Environment.NewLine);

            Regex bagsRegex = new Regex(@"((.+?)\sbags?)+?");
            Regex containsBagsRegex = new Regex(@"(?:contain)?\s(\d+)\s((.+?)\sbags?)");

            foreach (string bagString in bagsString)
            {
                MatchCollection bagMatches = bagsRegex.Matches(bagString);

                if (bagMatches.Count > 0)
                {
                    string bagType = bagMatches[0].Groups[2].Value;
                    Dictionary<string, int> containsBags = new Dictionary<string, int>();

                    for (int i = 1; i < bagMatches.Count; i++)
                    {
                        Match bagMatch = bagMatches[i];

                        MatchCollection containBagsMatches = containsBagsRegex.Matches(bagMatch.Groups[1].Value);
                        if (containBagsMatches.Count > 0)
                        {
                            foreach (Match containBagsMatch in containBagsMatches)
                            {
                                containsBags.Add(
                                    containBagsMatch.Groups[3].Value,
                                    int.Parse(containBagsMatch.Groups[1].Value)
                                );
                            }
                        }
                    }

                    bags.Add(bagType, containsBags);
                }
            }

            return bags;
        }
    }
}
