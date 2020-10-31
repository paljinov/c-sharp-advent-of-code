using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day19
{
    class Molecules
    {
        public int DisctinctMoleculesCount(string startingMolecule, List<(string, string)> replacements)
        {
            HashSet<string> molecules = CalculateDistinctMolecules(startingMolecule, replacements);
            return molecules.Count;
        }

        private HashSet<string> CalculateDistinctMolecules(string startingMolecule, List<(string, string)> replacements)
        {
            HashSet<string> molecules = new HashSet<string>();

            foreach ((string original, string replacement) in replacements)
            {
                // Split molecule by original part
                string[] moleculeSplitted = Regex.Split(startingMolecule, $"({original})");
                moleculeSplitted = moleculeSplitted.Where(part => !string.IsNullOrEmpty(part)).ToArray();

                // Iterate molecule parts
                for (int i = 0; i < moleculeSplitted.Length; i++)
                {
                    string part = moleculeSplitted[i];
                    // If this is original part which will be replaced
                    if (part == original)
                    {
                        // Create original molecule copy
                        string[] newMoleculeArray = new string[moleculeSplitted.Length];
                        Array.Copy(moleculeSplitted, newMoleculeArray, moleculeSplitted.Length);
                        // Make replacement
                        newMoleculeArray[i] = replacement;

                        // Create new molecule
                        string molecule = string.Join("", newMoleculeArray);
                        molecules.Add(molecule);
                    }
                }
            }

            return molecules;
        }
    }
}
