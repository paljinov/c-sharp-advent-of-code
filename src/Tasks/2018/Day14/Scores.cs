using System.Collections.Generic;

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
                CreateNewRecipes(recipes, ref firstElfPosition, ref secondElfPosition);
            }

            string scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes = "";
            for (int i = numberOfRecipes; i < numberOfRecipes + 10; i++)
            {
                scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes += recipes[i].ToString();
            }

            return scoresOfTenRecipesImmediatelyAfterInputNumberOfRecipes;
        }

        public int CountRecipesWhichAppearToTheLeftOfTheScoreSequence(int[] recipesScoresSequence)
        {
            int recipesWhichAppearToTheLeftOfTheScoreSequence = 0;

            int firstElfPosition = 0;
            int secondElfPosition = 1;

            List<int> recipes = new List<int> {
                FIRST_RECIPE,
                SECOND_RECIPE
            };

            int index = 0;
            int sequenceIndex = 0;

            while (recipesWhichAppearToTheLeftOfTheScoreSequence == 0)
            {
                CreateNewRecipes(recipes, ref firstElfPosition, ref secondElfPosition);

                // Check if score sequence exists in recipes scores
                while (index + sequenceIndex < recipes.Count)
                {
                    // If sequence score for index matches recipes score for index
                    if (recipesScoresSequence[sequenceIndex] == recipes[index + sequenceIndex])
                    {
                        // If complete score sequence is matched
                        if (sequenceIndex == recipesScoresSequence.Length - 1)
                        {
                            recipesWhichAppearToTheLeftOfTheScoreSequence = index;
                            break;
                        }
                        sequenceIndex++;
                    }
                    else
                    {
                        sequenceIndex = 0;
                        index++;
                    }
                }
            }

            return recipesWhichAppearToTheLeftOfTheScoreSequence;
        }

        private void CreateNewRecipes(List<int> recipes, ref int firstElfPosition, ref int secondElfPosition)
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
    }
}
