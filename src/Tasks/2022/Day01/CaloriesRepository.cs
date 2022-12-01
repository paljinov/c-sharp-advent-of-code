using System;

namespace App.Tasks.Year2022.Day1
{
    public class CaloriesRepository
    {
        public int[][] GetCaloriesPerElf(string input)
        {
            string[] caloriesPerElfString = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );
            int[][] caloriesPerElf = new int[caloriesPerElfString.Length][];

            for (int i = 0; i < caloriesPerElfString.Length; i++)
            {
                string[] caloriesString = caloriesPerElfString[i].Split(Environment.NewLine);

                int[] calories = new int[caloriesString.Length];
                for (int j = 0; j < caloriesString.Length; j++)
                {
                    calories[j] = int.Parse(caloriesString[j]);
                }

                caloriesPerElf[i] = calories;
            }

            return caloriesPerElf;
        }
    }
}
