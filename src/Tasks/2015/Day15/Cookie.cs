using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day15
{
    class Cookie
    {
        public const int TotalTeaspoons = 100;

        public const int TotalCalories = 500;

        /// <summary>
        /// Calculate highest cookie score.
        /// </summary>
        /// <param name="ingredients"></param>
        /// <param name="calorieCriterion"></param>
        /// <returns></returns>
        public int CalculateHighestScoringCookie(
            Dictionary<string, Ingredient> ingredients,
            bool calorieCriterion = false
        )
        {
            int highestScoringCookie = 0;

            List<Dictionary<string, int>> ingredientsPermutations = new List<Dictionary<string, int>>();
            Dictionary<string, int> teaspoons = new Dictionary<string, int>();
            foreach (KeyValuePair<string, Ingredient> ingredient in ingredients)
            {
                teaspoons.Add(ingredient.Key, 0);
            }

            GetIngredientsPermutations(ingredientsPermutations, teaspoons, -1);
            foreach (Dictionary<string, int> ingredientsPermutation in ingredientsPermutations)
            {
                if (calorieCriterion)
                {
                    int calories = CalculateCookieCalories(ingredientsPermutation, ingredients);
                    if (calories != TotalCalories)
                    {
                        continue;
                    }
                }

                int cookieScore = CalculateCookieScore(ingredientsPermutation, ingredients);

                highestScoringCookie = Math.Max(cookieScore, highestScoringCookie);
            }

            return highestScoringCookie;
        }

        /// <summary>
        /// Get all ingredients teaspoons valid permutations.
        /// </summary>
        /// <param name="ingredientsPermutations"></param>
        /// <param name="teaspoons"></param>
        /// <param name="currentIngredient"></param>
        private void GetIngredientsPermutations(
            List<Dictionary<string, int>> ingredientsPermutations,
            Dictionary<string, int> teaspoons,
            int currentIngredient
        )
        {
            List<string> ingredients = teaspoons.Keys.ToList();
            currentIngredient++;

            for (int i = 0; i <= TotalTeaspoons; i++)
            {
                teaspoons[ingredients[currentIngredient]] = i;

                // If current ingredient is not last recursive iterataton through ingredients is continued
                if (currentIngredient < ingredients.Count - 1)
                {
                    GetIngredientsPermutations(ingredientsPermutations, teaspoons, currentIngredient);
                }
                // If current ingredient is last check is made to be sure total number of teaspoons adds up to limit
                else
                {
                    int teaspoonsCount = teaspoons.Values.Sum();
                    if (teaspoonsCount == TotalTeaspoons)
                    {
                        ingredientsPermutations.Add(new Dictionary<string, int>(teaspoons));
                        // Set current ingredient to 0 teaspoons
                        teaspoons[ingredients[currentIngredient]] = 0;
                        // Iterator is increasing so teaspoons sum after this will always be above total limit
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Calculate total cookie score.
        /// </summary>
        /// <param name="ingredientsPermutation"></param>
        /// <param name="ingredients"></param>
        /// <returns></returns>
        private int CalculateCookieScore(
            Dictionary<string, int> ingredientsPermutation,
            Dictionary<string, Ingredient> ingredients
        )
        {
            int capacity = 0;
            int durability = 0;
            int flavor = 0;
            int texture = 0;

            foreach (KeyValuePair<string, int> ingredient in ingredientsPermutation)
            {
                string ingredientName = ingredient.Key;
                int teaspoonsCount = ingredient.Value;

                capacity += teaspoonsCount * ingredients[ingredientName].Capacity;
                durability += teaspoonsCount * ingredients[ingredientName].Durability;
                flavor += teaspoonsCount * ingredients[ingredientName].Flavor;
                texture += teaspoonsCount * ingredients[ingredientName].Texture;
            }

            int score = Math.Max(0, capacity) * Math.Max(0, durability)
                * Math.Max(0, flavor) * Math.Max(0, texture);

            return score;
        }

        /// <summary>
        /// Calculate cookie calories.
        /// </summary>
        /// <param name="ingredientsPermutation"></param>
        /// <param name="ingredients"></param>
        /// <returns></returns>
        private int CalculateCookieCalories(
           Dictionary<string, int> ingredientsPermutation,
           Dictionary<string, Ingredient> ingredients
       )
        {
            int calories = 0;

            foreach (KeyValuePair<string, int> ingredient in ingredientsPermutation)
            {
                string ingredientName = ingredient.Key;
                int teaspoonsCount = ingredient.Value;

                calories += teaspoonsCount * ingredients[ingredientName].Calories;
            }

            return calories;
        }
    }
}
