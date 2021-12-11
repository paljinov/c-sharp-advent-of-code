using System;

namespace App.Tasks.Year2021.Day11
{
    public class OctopusesEnergyLevelsRepository
    {
        public int[,] GetOctopusesEnergyLevels(string input)
        {
            string[] octopusesEnergyLevelsString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = octopusesEnergyLevelsString.Length;
            int columns = octopusesEnergyLevelsString[0].Length;
            int[,] octopusesEnergyLevels = new int[rows, columns];

            for (int i = 0; i < octopusesEnergyLevelsString.Length; i++)
            {
                for (int j = 0; j < octopusesEnergyLevelsString[i].Length; j++)
                {

                    octopusesEnergyLevels[i, j] = (int)char.GetNumericValue(octopusesEnergyLevelsString[i][j]);
                }
            }

            return octopusesEnergyLevels;
        }
    }
}
