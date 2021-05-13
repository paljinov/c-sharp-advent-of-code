using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day14
{
    public class Scores
    {
        private const int FIRST_RECIPE = 3;

        private const int SECOND_RECIPE = 7;

        public string CalculateScoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes(int numberOfRecipes)
        {
            int firstElfPosition = 0;
            int secondElfPosition = 1;

            List<int> recipes = new List<int> {
                FIRST_RECIPE,
                SECOND_RECIPE
            };

            while (recipes.Count < numberOfRecipes + 10)
            {
                int recipesSum = recipes[firstElfPosition] + recipes[secondElfPosition];
                // If recipe sum has two digits
                if (recipesSum >= 10)
                {
                    recipes.Add(recipesSum / 10);
                }
                recipes.Add(recipesSum % 10);

                // Move elves
                firstElfPosition = (firstElfPosition + recipes[firstElfPosition] + 1) % recipes.Count;
                secondElfPosition = (secondElfPosition + recipes[secondElfPosition] + 1) % recipes.Count;
            }

            string scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes = "";
            for (int i = numberOfRecipes; i < numberOfRecipes + 10; i++)
            {
                scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes += recipes[i].ToString();
            }

            return scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes;
        }
    }
}
