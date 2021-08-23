/*
--- Part Two ---

After collecting ORE for a while, you check your cargo hold: 1 trillion
(1000000000000) units of ORE.

With that much ore, given the examples above:

- The 13312 ORE-per-FUEL example could produce 82892753 FUEL.
- The 180697 ORE-per-FUEL example could produce 5586022 FUEL.
- The 2210736 ORE-per-FUEL example could produce 460664 FUEL.

Given 1 trillion ORE, what is the maximum amount of FUEL you can produce?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2019.Day14
{
    public class Part2 : ITask<long>
    {
        private const long TOTAL_ORE = 1000000000000;

        private readonly ReactionsRepository reactionsRepository;

        private readonly FuelNanofactory fuelNanofactory;

        public Part2()
        {
            reactionsRepository = new ReactionsRepository();
            fuelNanofactory = new FuelNanofactory();
        }
        public long Solution(string input)
        {
            Dictionary<string, Reaction> reactions = reactionsRepository.GetReactions(input);
            long maximumAmountOfFuel =
                fuelNanofactory.CalculateMaximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOre(reactions, TOTAL_ORE);

            return maximumAmountOfFuel;
        }
    }
}
