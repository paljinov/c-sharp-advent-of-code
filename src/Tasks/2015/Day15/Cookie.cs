using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day15
{
    class Cookie
    {
        public const int TotalTeaspoons = 100;

        public int GetHighestScoringCookie(Dictionary<string, Ingredient> ingredients)
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
                int cookieScore = CalculateCookieScore(ingredientsPermutation, ingredients);
                highestScoringCookie = Math.Max(cookieScore, highestScoringCookie);
            }

            return highestScoringCookie;
        }

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

                if (currentIngredient < ingredients.Count - 1)
                {
                    GetIngredientsPermutations(ingredientsPermutations, teaspoons, currentIngredient);
                }

                int teaspoonsCount = teaspoons.Values.Sum();
                // If the amounts of each ingredient add up to specified number of teaspoons
                if (teaspoonsCount == TotalTeaspoons)
                {
                    ingredientsPermutations.Add(new Dictionary<string, int>(teaspoons));
                    // Set current ingredient to 0 teaspoons
                    teaspoons[ingredients[currentIngredient]] = 0;
                    // Iterator is increasing so teaspoons sum will always be above total limit
                    break;
                }
            }
        }

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
    }
}
