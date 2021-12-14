/*
--- Part Two ---

The resulting polymer isn't nearly strong enough to reinforce the submarine.
You'll need to run more steps of the pair insertion process; a total of 40 steps
should do it.

In the above example, the most common element is B (occurring 2192039569602
times) and the least common element is H (occurring 3849876073 times);
subtracting these produces 2188189693529.

Apply 40 steps of pair insertion to the polymer template and find the most and
least common elements in the result. What do you get if you take the quantity of
the most common element and subtract the quantity of the least common element?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2021.Day14
{
    public class Part2 : ITask<long>
    {
        private const int TOTAL_STEPS = 40;

        private readonly PolymerInstructionsRepository polymerInstructionsRepository;

        private readonly Polymer polymer;

        public Part2()
        {
            polymerInstructionsRepository = new PolymerInstructionsRepository();
            polymer = new Polymer();
        }

        public long Solution(string input)
        {
            string polymerTemplate = polymerInstructionsRepository.GetPolymerTemplate(input);
            Dictionary<string, char> pairInsertionRules = polymerInstructionsRepository.GetPairInsertionRules(input);

            long differenceBetweenMostAndLeastCommonElement = polymer
                .CalculateDifferenceBetweenMostAndLeastCommonElement(polymerTemplate, pairInsertionRules, TOTAL_STEPS);

            return differenceBetweenMostAndLeastCommonElement;
        }
    }
}
