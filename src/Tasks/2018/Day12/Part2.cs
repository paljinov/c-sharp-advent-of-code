/*
--- Part Two ---

You realize that 20 generations aren't enough. After all, these plants will need
to last another 1500 years to even reach your timeline, not to mention your
future.

After fifty billion (50000000000) generations, what is the sum of the numbers of
all pots which contain a plant?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2018.Day12
{
    public class Part2 : ITask<long>
    {
        private const long GENERATIONS = 50000000000;

        private readonly PlantsRepository plantsRepository;

        private readonly PlantsGenerations plantsGenerations;

        public Part2()
        {
            plantsRepository = new PlantsRepository();
            plantsGenerations = new PlantsGenerations();
        }
        public long Solution(string input)
        {
            string pots = plantsRepository.GetPots(input);
            Dictionary<string, char> spreadNotes = plantsRepository.GetSpreadNotes(input);
            long sumOfTheNumbersOfAllPotsWhichContainPlantAfterGenerations = plantsGenerations
                .CalculateSumOfTheNumbersOfAllPotsWhichContainPlantAfterGenerations(pots, spreadNotes, GENERATIONS);

            return sumOfTheNumbersOfAllPotsWhichContainPlantAfterGenerations;
        }
    }
}
