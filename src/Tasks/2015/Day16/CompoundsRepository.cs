using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day16
{
    public class CompoundsRepository
    {
        public readonly Compounds AuntSueCompounds = new Compounds
        {
            Children = 3,
            Cats = 7,
            Samoyeds = 2,
            Pomeranians = 3,
            Akitas = 0,
            Vizslas = 0,
            Goldfish = 5,
            Trees = 3,
            Cars = 2,
            Perfumes = 1
        };

        public Dictionary<int, Compounds> ParseInput(string input)
        {
            Dictionary<int, Compounds> compounds = new Dictionary<int, Compounds>();

            string[] suesCompoundsStrings =
               input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Regex sueCompoundsRegex = new Regex(@"^Sue (\d+): (\w+: \d+), (\w+: \d+), (\w+: \d+)$");

            foreach (string sueCompoundsString in suesCompoundsStrings)
            {
                Match match = sueCompoundsRegex.Match(sueCompoundsString);
                GroupCollection groups = match.Groups;

                int sue = int.Parse(groups[1].Value);
                Compounds sueCompounds = new Compounds();

                for (int i = 2; i < groups.Count; i++)
                {
                    string[] compound = groups[i].Value.Split(": ");
                    switch (compound[0])
                    {
                        case "children":
                            sueCompounds.Children = int.Parse(compound[1]);
                            break;
                        case "cats":
                            sueCompounds.Cats = int.Parse(compound[1]);
                            break;
                        case "samoyeds":
                            sueCompounds.Samoyeds = int.Parse(compound[1]);
                            break;
                        case "pomeranians":
                            sueCompounds.Pomeranians = int.Parse(compound[1]);
                            break;
                        case "akitas":
                            sueCompounds.Akitas = int.Parse(compound[1]);
                            break;
                        case "vizslas":
                            sueCompounds.Vizslas = int.Parse(compound[1]);
                            break;
                        case "goldfish":
                            sueCompounds.Goldfish = int.Parse(compound[1]);
                            break;
                        case "trees":
                            sueCompounds.Trees = int.Parse(compound[1]);
                            break;
                        case "cars":
                            sueCompounds.Cars = int.Parse(compound[1]);
                            break;
                        case "perfumes":
                            sueCompounds.Perfumes = int.Parse(compound[1]);
                            break;
                    }
                }

                compounds.Add(sue, sueCompounds);
            }

            return compounds;
        }
    }
}
