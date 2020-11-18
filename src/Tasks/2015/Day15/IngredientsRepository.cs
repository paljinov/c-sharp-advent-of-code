using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day15
{
    class IngredientsRepository
    {
        public Dictionary<string, Ingredient> ParseInput(string input)
        {
            Dictionary<string, Ingredient> ingredients = new Dictionary<string, Ingredient>();

            string[] ingredientStrings =
               input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Regex ingredientsRegex = new Regex(
                @"^(\w+): capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)$"
            );

            foreach (string ingredientString in ingredientStrings)
            {
                Match match = ingredientsRegex.Match(ingredientString);
                GroupCollection groups = match.Groups;

                Ingredient ingredient = new Ingredient
                {
                    Capacity = int.Parse(groups[2].Value),
                    Durability = int.Parse(groups[3].Value),
                    Flavor = int.Parse(groups[4].Value),
                    Texture = int.Parse(groups[5].Value),
                    Calories = int.Parse(groups[6].Value)
                };

                ingredients.Add(groups[1].Value, ingredient);
            }

            return ingredients;
        }
    }
}
