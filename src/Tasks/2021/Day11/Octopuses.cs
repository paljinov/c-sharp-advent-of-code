using System.Collections.Generic;

namespace App.Tasks.Year2021.Day11
{
    public class Octopuses
    {
        private const int ENERGY_LEVEL_INCREMENT = 1;

        private const int ENERGY_LEVEL_AFTER_FLASH = 0;

        private const int FLASH_TRESHOLD = 9;

        public int CountTotalFlashesAfterGivenSteps(int[,] octopusesEnergyLevels, int totalSteps)
        {
            int totalFlashes = 0;

            for (int i = 1; i <= totalSteps; i++)
            {
                totalFlashes += DoStep(octopusesEnergyLevels);
            }

            return totalFlashes;
        }

        private int DoStep(int[,] octopusesEnergyLevels)
        {
            int stepFlashes = 0;

            // An octopus can only flash at most once per step
            HashSet<(int X, int Y)> octopusesWhichFlashed = new HashSet<(int X, int Y)>();

            for (int i = 0; i < octopusesEnergyLevels.GetLength(0); i++)
            {
                for (int j = 0; j < octopusesEnergyLevels.GetLength(1); j++)
                {
                    stepFlashes += IncreaseOctopusEnergyLevel(i, j, octopusesEnergyLevels, octopusesWhichFlashed);
                }
            }

            // Any octopus that flashed during this step has its energy level set to 0
            foreach ((int i, int j) in octopusesWhichFlashed)
            {
                octopusesEnergyLevels[i, j] = ENERGY_LEVEL_AFTER_FLASH;
            }

            return stepFlashes;
        }

        private int IncreaseOctopusEnergyLevel(
            int i,
            int j,
            int[,] octopusesEnergyLevels,
            HashSet<(int X, int Y)> octopusesWhichFlashed
        )
        {
            int octopusFlash = 0;

            octopusesEnergyLevels[i, j] += ENERGY_LEVEL_INCREMENT;
            if (octopusesEnergyLevels[i, j] > FLASH_TRESHOLD && !octopusesWhichFlashed.Contains((i, j)))
            {
                octopusesWhichFlashed.Add((i, j));
                octopusFlash++;
                octopusFlash +=
                    IncreaseAdjacentOctopusesEnergyLevels(i, j, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            return octopusFlash;
        }

        private int IncreaseAdjacentOctopusesEnergyLevels(
            int i,
            int j,
            int[,] octopusesEnergyLevels,
            HashSet<(int X, int Y)> octopusesWhichFlashed
        )
        {
            int adjacentOctopusesFlashes = 0;

            // Top left
            if (i - 1 >= 0 && j - 1 >= 0)
            {
                adjacentOctopusesFlashes +=
                    IncreaseOctopusEnergyLevel(i - 1, j - 1, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            // Top
            if (i - 1 >= 0)
            {
                adjacentOctopusesFlashes +=
                    IncreaseOctopusEnergyLevel(i - 1, j, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            // Top right
            if (i - 1 >= 0 && j + 1 < octopusesEnergyLevels.GetLength(1))
            {
                adjacentOctopusesFlashes +=
                    IncreaseOctopusEnergyLevel(i - 1, j + 1, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            // Right
            if (j + 1 < octopusesEnergyLevels.GetLength(1))
            {
                adjacentOctopusesFlashes +=
                    IncreaseOctopusEnergyLevel(i, j + 1, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            // Bottom right
            if (i + 1 < octopusesEnergyLevels.GetLength(0) && j + 1 < octopusesEnergyLevels.GetLength(1))
            {
                adjacentOctopusesFlashes +=
                 IncreaseOctopusEnergyLevel(i + 1, j + 1, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            // Bottom
            if (i + 1 < octopusesEnergyLevels.GetLength(0))
            {
                adjacentOctopusesFlashes +=
                    IncreaseOctopusEnergyLevel(i + 1, j, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            // Bottom left
            if (i + 1 < octopusesEnergyLevels.GetLength(0) && j - 1 >= 0)
            {
                adjacentOctopusesFlashes +=
                   IncreaseOctopusEnergyLevel(i + 1, j - 1, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            // Left
            if (j - 1 >= 0)
            {
                adjacentOctopusesFlashes +=
                    IncreaseOctopusEnergyLevel(i, j - 1, octopusesEnergyLevels, octopusesWhichFlashed);
            }

            return adjacentOctopusesFlashes;
        }
    }
}
