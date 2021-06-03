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
    public class Part2 : ITask<int>
    {
        private readonly CompoundsRepository compoundsRepository;
        private readonly AuntSue auntSue;

        public Part2()
        {
            compoundsRepository = new CompoundsRepository();
            auntSue = new AuntSue();
        }

        public int Solution(string input)
        {
            Dictionary<int, Compounds> suesCompounds = compoundsRepository.ParseInput(input);
            int numberOfTheSueThatSentTheGift = auntSue.FindNumberOfTheSueThatSentTheGift(suesCompounds, true);

            return numberOfTheSueThatSentTheGift;
        }
    }
}
