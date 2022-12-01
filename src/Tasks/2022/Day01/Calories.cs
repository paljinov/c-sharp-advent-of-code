using System;
using System.Linq;

namespace App.Tasks.Year2022.Day1
{
    public class Calories
    {
        public int CalculateTotalCaloriesForElfCarryingTheMost(int[][] caloriesPerElf)
        {
            int totalCaloriesForElfCarryingTheMost = 0;

            int measurementsWhichAreLargerThanThePreviousCalorie = 0;

            for (int i = 0; i < caloriesPerElf.Length; i++)
            {
                int totalCalories = caloriesPerElf[i].Sum();
                totalCaloriesForElfCarryingTheMost = Math.Max(totalCaloriesForElfCarryingTheMost, totalCalories);
            }

            return totalCaloriesForElfCarryingTheMost;
        }
    }
}
