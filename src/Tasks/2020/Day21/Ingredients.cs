using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day21
{
    public class Ingredients
    {
        public int CountAppearancesOfIngredientsWithoutAllergens(List<Food> foods)
        {
            int appearancesOfIngredientsWithoutAllergens = 0;

            Dictionary<string, string> ingredientsWithTheirAllergen = GetIngredientsWithTheirAllergen(foods);

            foreach (Food food in foods)
            {
                foreach (string ingredient in food.Ingredients)
                {
                    if (!ingredientsWithTheirAllergen.ContainsKey(ingredient))
                    {
                        appearancesOfIngredientsWithoutAllergens++;
                    }
                }
            }

            return appearancesOfIngredientsWithoutAllergens;
        }

        public string GetCommaSeparatedDangerousIngredientsSortedByAllergen(List<Food> foods)
        {
            Dictionary<string, string> ingredientsWithTheirAllergen = GetIngredientsWithTheirAllergen(foods);
            // Sort ingredients alphabetically by their allergen
            ingredientsWithTheirAllergen = ingredientsWithTheirAllergen
                .OrderBy(i => i.Value)
                .ToDictionary(i => i.Key, i => i.Value);

            // Separate ingredients by commas
            string dangerousIngredients = string.Join(",", ingredientsWithTheirAllergen.Keys);

            return dangerousIngredients;
        }

        private Dictionary<string, string> GetIngredientsWithTheirAllergen(List<Food> foods)
        {
            Dictionary<string, string> ingredientsWithTheirAllergen = new Dictionary<string, string>();

            Dictionary<string, HashSet<string>> possibleIngredientsWithAllergen =
                GetPossibleIngredientsWithAllergen(foods);

            while (ingredientsWithTheirAllergen.Count < possibleIngredientsWithAllergen.Count)
            {
                foreach (KeyValuePair<string, HashSet<string>> allergenIngredients in possibleIngredientsWithAllergen)
                {
                    // Each allergen is found in exactly one ingredient,
                    // so if ingredient is found for current allergen check is skipped
                    if (!ingredientsWithTheirAllergen.ContainsValue(allergenIngredients.Key))
                    {
                        foreach (string ingredient in allergenIngredients.Value)
                        {
                            // If it is already concluded that this ingredient has other allergen
                            // it is removed from possible ingredients for this allergen
                            if (ingredientsWithTheirAllergen.ContainsKey(ingredient))
                            {
                                allergenIngredients.Value.Remove(ingredient);
                            }
                        }

                        if (allergenIngredients.Value.Count == 1)
                        {
                            ingredientsWithTheirAllergen.Add(
                                allergenIngredients.Value.First(),
                                allergenIngredients.Key
                            );
                        }
                    }
                }
            }

            return ingredientsWithTheirAllergen;
        }

        private Dictionary<string, HashSet<string>> GetPossibleIngredientsWithAllergen(List<Food> foods)
        {
            Dictionary<string, HashSet<string>> possibleIngredientsWithAllergen =
                new Dictionary<string, HashSet<string>>();

            foreach (Food food in foods)
            {
                foreach (string allergen in food.Allergens)
                {
                    // If allergen key doesn't exists
                    if (!possibleIngredientsWithAllergen.ContainsKey(allergen))
                    {
                        possibleIngredientsWithAllergen.Add(allergen, food.Ingredients.ToHashSet());
                    }
                    else
                    {
                        // Keep only ingredients which could have this allergen
                        possibleIngredientsWithAllergen[allergen].IntersectWith(food.Ingredients);
                    }
                }
            }

            return possibleIngredientsWithAllergen;
        }
    }
}
