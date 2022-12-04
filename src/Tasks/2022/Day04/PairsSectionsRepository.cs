using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2022.Day4
{
    public class PairsSectionsRepository
    {
        public Pair[] GetPairsSections(string input)
        {
            string[] pairsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Pair[] pairsSections = new Pair[pairsString.Length];

            Regex pairSectionsRegex = new Regex(@"^(\d+)-(\d+),(\d+)-(\d+)$");

            for (int i = 0; i < pairsString.Length; i++)
            {
                Match pairSectionsMatch = pairSectionsRegex.Match(pairsString[i]);
                GroupCollection pairSectionsGroups = pairSectionsMatch.Groups;

                Pair pair = new Pair
                {
                    FirstSectionsRange = (
                        int.Parse(pairSectionsGroups[1].Value),
                        int.Parse(pairSectionsGroups[2].Value)
                    ),
                    SecondSectionsRange = (
                        int.Parse(pairSectionsGroups[3].Value),
                        int.Parse(pairSectionsGroups[4].Value)
                    ),
                };

                pairsSections[i] = pair;
            }

            return pairsSections;
        }
    }
}
