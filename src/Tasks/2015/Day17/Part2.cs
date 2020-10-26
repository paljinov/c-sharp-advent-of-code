/*
--- Part Two ---

While playing with all the containers in the kitchen, another load of eggnog
arrives! The shipping and receiving department is requesting as many containers
as you can spare.

Find the minimum number of containers that can exactly fit all 150 liters of
eggnog. How many different ways can you fill that number of containers and still
hold exactly 150 litres?

In the example above, the minimum number of containers was two. There were three
ways to use that many containers, and so the answer there would be 3.
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day17
{
    class Part2 : ITask<int>
    {
        private readonly ContainersRepository containersRepository;

        private readonly ContainersCombinations containersCombinations;

        public Part2()
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

            int minUsedContainers = int.MaxValue;
            int minUsedContainersDifferentWays = 0;

            foreach (List<int> combination in combinations)
            {
                if (combination.Count < minUsedContainers)
                {
                    minUsedContainers = combination.Count;
                    minUsedContainersDifferentWays = 1;
                }
                else if (combination.Count == minUsedContainers)
                {
                    minUsedContainersDifferentWays++;
                }
            }

            return minUsedContainersDifferentWays;
        }
    }
}
