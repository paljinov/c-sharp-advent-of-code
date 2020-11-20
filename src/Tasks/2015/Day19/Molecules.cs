using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day19
{
    public class Molecules
    {
        public int CountCreatedDisctinctMolecules(string startingMolecule, List<(string, string)> replacements)
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

            return molecules.Count;
        }

        public int DecomposeMoleculeToSingleElectron(
            string molecule,
            List<(string, string)> replacements,
            string singleElectron
        )
        {
            int steps = 0;
            while (molecule != singleElectron)
            {
                // Replace in opposite direction to single electron
                foreach ((string replacement, string original) in replacements)
                {
                    var regex = new Regex(original);
                    if (regex.Match(molecule).Success)
                    {
                        // Replace only one occurence
                        molecule = regex.Replace(molecule, replacement, 1);
                        steps++;
                    }
                }
            }

            return steps;
        }
    }
}
