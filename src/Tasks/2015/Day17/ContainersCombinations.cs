using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day17
{
    class ContainersCombinations
    {
        public const int EggnogLiters = 150;

        public void CalculateContainersCombinations(
            int[] containers,
            int litersRemaining,
            List<int> currentCombination,
            List<List<int>> combinations
        )
        {
            if (containers.Length == 0)
            {
                currentCombination.Clear();
                return;
            }

            int container = containers[0];
            // Remove first container from the containers array
            containers = containers.Skip(1).ToArray();

            // If valid combination is finalized with this container
            if (container == litersRemaining)
            {
                combinations.Add(new List<int>(currentCombination) { container });
            }
            // If more containers need to be used to fit all liters of eggnog
            else if (container < litersRemaining)
            {
                CalculateContainersCombinations(
                    containers,
                    litersRemaining - container,
                    new List<int>(currentCombination) { container },
                    combinations
                );
            }

            CalculateContainersCombinations(containers, litersRemaining, currentCombination, combinations);
        }
    }
}
