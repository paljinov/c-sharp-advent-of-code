/*
--- Day 17: No Such Thing as Too Much ---

The elves bought too much eggnog again - 150 liters this time. To fit it all
into your refrigerator, you'll need to move it into smaller containers. You take
an inventory of the capacities of the available containers.

For example, suppose you have containers of size 20, 15, 10, 5, and 5 liters. If
you need to store 25 liters, there are four ways to do it:

- 15 and 10
- 20 and 5 (the first 5)
- 20 and 5 (the second 5)
- 15, 5, and 5

Filling all containers entirely, how many different combinations of containers
can exactly fit all 150 liters of eggnog?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day17
{
    class Part1 : ITask<int>
    {
        private readonly ContainersRepository containersRepository;

        private readonly ContainersCombinations containersCombinations;

        public Part1()
        {
            containersRepository = new ContainersRepository();
            containersCombinations = new ContainersCombinations();
        }

        public int Solution(string input)
        {
            int[] containers = containersRepository.ParseInput(input);

            List<List<int>> combinations = new List<List<int>>();
            List<int> currentCombination = new List<int>();
            containersCombinations.CalculateContainersCombinations(
                containers,
                ContainersCombinations.EggnogLiters,
                currentCombination,
                combinations
            );

            int differentCombinations = combinations.Count;

            return differentCombinations;
        }
    }
}
