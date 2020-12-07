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

            string[] bagsRulesString = input.Split(Environment.NewLine);

            Regex bagsRegex = new Regex(@"(.+?)\sbags?");
            Regex containedBagsRegex = new Regex(@"(\d+)\s(.+)");

            foreach (string bagRuleString in bagsRulesString)
            {
                MatchCollection bagMatches = bagsRegex.Matches(bagRuleString);
                if (bagMatches.Count > 0)
                {
                    string bagColor = bagMatches[0].Groups[1].Value;
                    Dictionary<string, int> containedBags = new Dictionary<string, int>();

                    for (int i = 1; i < bagMatches.Count; i++)
                    {
                        MatchCollection containedBagsMatches =
                            containedBagsRegex.Matches(bagMatches[i].Groups[1].Value);

                        if (containedBagsMatches.Count > 0)
                        {
                            foreach (Match containedBagsMatch in containedBagsMatches)
                            {
                                string containedBagColor = containedBagsMatch.Groups[2].Value;
                                int containedBagQuantity = int.Parse(containedBagsMatch.Groups[1].Value);

                                containedBags.Add(containedBagColor, containedBagQuantity);
                            }
                        }
                    }

                    bags.Add(bagColor, containedBags);
                }
            }

            return bags;
        }
    }
}
