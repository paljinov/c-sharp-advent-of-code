using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace App.Tasks.Year2015.Day15
{
    class Cookie
    {
        public const int TotalTeaspoons = 100;

        public int GetHighestScoringCookie(Dictionary<string, Ingredient> ingredientsDictionary)
        {
            int highestScoringCookie = 0;

            List<Ingredient> ingredients = ingredientsDictionary.Values.ToList();

            for (int i = 0; i < TotalTeaspoons; i++)
            {
                for (int j = 0; j < TotalTeaspoons; j++)
                {
                    for (int k = 0; k < TotalTeaspoons; k++)
                    {
                        for (int h = 0; h < TotalTeaspoons; h++)
                        {
                            if (i + j + k + h == TotalTeaspoons)
                            {
                                int capacity = Math.Max(0, i * ingredients[0].Capacity + j * ingredients[1].Capacity
                                    + k * ingredients[2].Capacity + h * ingredients[3].Capacity);
                                int durability = Math.Max(0, i * ingredients[0].Durability + j * ingredients[1].Durability
                                    + k * ingredients[2].Durability + h * ingredients[3].Durability);
                                int flavor = Math.Max(0, i * ingredients[0].Flavor + j * ingredients[1].Flavor
                                    + k * ingredients[2].Flavor + h * ingredients[3].Flavor);
                                int texture = Math.Max(0, i * ingredients[0].Texture + j * ingredients[1].Texture
                                    + k * ingredients[2].Texture + h * ingredients[3].Texture);

                                int product = capacity * durability * flavor * texture;

                                highestScoringCookie = Math.Max(product, highestScoringCookie);
                            }
                        }
                    }
                }
            }

            return highestScoringCookie;
        }
    }
}
