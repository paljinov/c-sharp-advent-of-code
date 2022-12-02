using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2022.Day1
{
    public class Calories
    {
        public int CalculateTotalCaloriesForElfCarryingTheMost(int[][] caloriesPerElf)
        {
            Dictionary<int, int> totalCaloriesPerElf = CalculateTotalCaloriesPerElf(caloriesPerElf);
            int totalCaloriesForElfCarryingTheMost = totalCaloriesPerElf.Values.Max();

            return totalCaloriesForElfCarryingTheMost;
        }

        public int CalculateTotalCaloriesForTopThreeElvesCarryingTheMost(int[][] caloriesPerElf)
        {
            Dictionary<int, int> totalCaloriesPerElf = CalculateTotalCaloriesPerElf(caloriesPerElf);
            int totalCaloriesForTopThreeElvesCarryingTheMost = totalCaloriesPerElf.OrderByDescending(c => c.Value)
                .Take(3).ToDictionary(c => c.Key, c => c.Value)
                .Values.Sum();

            return totalCaloriesForTopThreeElvesCarryingTheMost;
        }

        private Dictionary<int, int> CalculateTotalCaloriesPerElf(int[][] caloriesPerElf)
        {
            Dictionary<int, int> totalCaloriesPerElf = new Dictionary<int, int>();
            for (int elf = 0; elf < caloriesPerElf.Length; elf++)
            {
                totalCaloriesPerElf[elf] = caloriesPerElf[elf].Sum();
            }

            return totalCaloriesPerElf;
        }
    }
}
