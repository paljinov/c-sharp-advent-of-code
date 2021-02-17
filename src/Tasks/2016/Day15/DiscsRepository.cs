using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day15
{
    public class DiscsRepository
    {
        public Disc[] GetDiscs(string input)
        {
            string[] discsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Disc[] discs = new Disc[discsString.Length];

            Regex discRegex = new Regex(@"^Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+).$");

            for (int i = 0; i < discsString.Length; i++)
            {
                Match match = discRegex.Match(discsString[i]);
                GroupCollection groups = match.Groups;

                discs[i] = new Disc
                {
                    Number = int.Parse(groups[1].Value),
                    TotalPositions = int.Parse(groups[2].Value),
                    InitialPosition = int.Parse(groups[3].Value)
                };
            }

            return discs;
        }
    }
}
