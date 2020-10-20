/*
--- Part Two ---

As you're about to send the thank you note, something in the MFCSAM's
instructions catches your eye. Apparently, it has an outdated retroencabulator,
and so the output from the machine isn't exact values - some of them indicate
ranges.

In particular, the cats and trees readings indicates that there are greater than
that many (due to the unpredictable nuclear decay of cat dander and tree
pollen), while the pomeranians and goldfish readings indicate that there are
fewer than that many (due to the modial interaction of magnetoreluctance).

What is the number of the real Aunt Sue?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day16
{
    class Part2 : ITask<int>
    {
        private readonly CompoundsRepository compoundsRepository;

        public Part2()
        {
            compoundsRepository = new CompoundsRepository();
        }

        public int Solution(string input)
        {
            int sue = 0;

            Dictionary<int, Compounds> suesCompounds = compoundsRepository.ParseInput(input);
            foreach (KeyValuePair<int, Compounds> compounds in suesCompounds)
            {
                if (IsAuntSue(compounds.Value))
                {
                    sue = compounds.Key;
                    break;
                }
            }

            return sue;
        }

        private bool IsAuntSue(Compounds compounds)
        {
            if (compounds.Children != null && compounds.Children != compoundsRepository.AuntSueCompounds.Children)
            {
                return false;
            }
            if (compounds.Cats != null && compounds.Cats <= compoundsRepository.AuntSueCompounds.Cats)
            {
                return false;
            }
            if (compounds.Samoyeds != null && compounds.Samoyeds != compoundsRepository.AuntSueCompounds.Samoyeds)
            {
                return false;
            }
            if (compounds.Pomeranians != null && compounds.Pomeranians >= compoundsRepository.AuntSueCompounds.Pomeranians)
            {
                return false;
            }
            if (compounds.Akitas != null && compounds.Akitas != compoundsRepository.AuntSueCompounds.Akitas)
            {
                return false;
            }
            if (compounds.Vizslas != null && compounds.Vizslas != compoundsRepository.AuntSueCompounds.Vizslas)
            {
                return false;
            }
            if (compounds.Goldfish != null && compounds.Goldfish >= compoundsRepository.AuntSueCompounds.Goldfish)
            {
                return false;
            }
            if (compounds.Trees != null && compounds.Trees <= compoundsRepository.AuntSueCompounds.Trees)
            {
                return false;
            }
            if (compounds.Cars != null && compounds.Cars != compoundsRepository.AuntSueCompounds.Cars)
            {
                return false;
            }
            if (compounds.Perfumes != null && compounds.Perfumes != compoundsRepository.AuntSueCompounds.Perfumes)
            {
                return false;
            }

            return true;
        }
    }
}
