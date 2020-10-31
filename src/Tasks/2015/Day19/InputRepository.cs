using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day19
{
    class InputRepository
    {
        public string GetStartingMolecule(string input)
        {
            Regex startingMoleculeRegex = new Regex(@"(\w+)$");
            Match startingMoleculeMatch = startingMoleculeRegex.Match(input);

            string startingMolecule = startingMoleculeMatch.Groups[1].Value;

            return startingMolecule;
        }

        public List<(string, string)> GetReplacements(string input)
        {
            List<(string, string)> replacements = new List<(string, string)>();

            string[] replacementsStrings = input.Split(Environment.NewLine);
            Regex replacementsRegex = new Regex(@"(\w+)\s=>\s(\w+)");

            foreach (string replacementString in replacementsStrings)
            {
                Match replacementMatches = replacementsRegex.Match(replacementString);
                if (replacementMatches.Success)
                {
                    GroupCollection groups = replacementMatches.Groups;
                    replacements.Add((groups[1].Value, groups[2].Value));
                }
            }

            return replacements;
        }
    }
}
