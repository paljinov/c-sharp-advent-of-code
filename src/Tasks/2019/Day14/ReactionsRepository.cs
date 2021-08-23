using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2019.Day14
{
    public class ReactionsRepository
    {
        public Dictionary<string, Reaction> GetReactions(string input)
        {
            Dictionary<string, Reaction> reactions = new Dictionary<string, Reaction>();

            string[] reactionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex reactionRegex = new Regex(@"(\d+\s\w+)");

            for (int i = 0; i < reactionsString.Length; i++)
            {
                List<Chemical> chemicals = new List<Chemical>();

                MatchCollection reactionMatches = reactionRegex.Matches(reactionsString[i]);
                foreach (Match reactionMatch in reactionMatches)
                {
                    GroupCollection reactionGroups = reactionMatch.Groups;
                    string[] parts = reactionGroups[0].Value.Split(' ', StringSplitOptions.TrimEntries);
                    Chemical chemical = new Chemical
                    {
                        Name = parts[1],
                        Amount = int.Parse(parts[0])
                    };

                    chemicals.Add(chemical);
                }

                Chemical outputChemical = chemicals[^1];
                chemicals.RemoveAt(chemicals.Count - 1);

                Reaction reaction = new Reaction
                {
                    InputChemicals = chemicals,
                    OutputChemical = outputChemical
                };

                reactions.Add(outputChemical.Name, reaction);
            }

            return reactions;
        }
    }
}
