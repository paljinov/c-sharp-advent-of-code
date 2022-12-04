using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2022.Day4
{
    public class SectionAssignmentPairsRepository
    {
        public Pair[] GetSectionAssignmentPairs(string input)
        {
            string[] sectionAssignmentPairsString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Pair[] sectionAssignmentPairs = new Pair[sectionAssignmentPairsString.Length];

            Regex sectionAssignmentPairsRegex = new Regex(@"^(\d+)-(\d+),(\d+)-(\d+)$");

            for (int i = 0; i < sectionAssignmentPairsString.Length; i++)
            {
                Match sectionAssignmentPairsMatch = sectionAssignmentPairsRegex.Match(sectionAssignmentPairsString[i]);
                GroupCollection sectionAssignmentPairsGroups = sectionAssignmentPairsMatch.Groups;

                Pair pair = new Pair
                {
                    FirstSectionsRange = (
                        int.Parse(sectionAssignmentPairsGroups[1].Value),
                        int.Parse(sectionAssignmentPairsGroups[2].Value)
                    ),
                    SecondSectionsRange = (
                        int.Parse(sectionAssignmentPairsGroups[3].Value),
                        int.Parse(sectionAssignmentPairsGroups[4].Value)
                    ),
                };

                sectionAssignmentPairs[i] = pair;
            }

            return sectionAssignmentPairs;
        }
    }
}
