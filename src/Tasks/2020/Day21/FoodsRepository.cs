using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day21
{
    public class FoodsRepository
    {
        public List<Food> GetFoods(string input)
        {
            List<Food> foods = new List<Food>();

            Regex foodRegex = new Regex(@"^([a-z\s]+?)\s\(contains\s([a-z\s,]+?)\)$");

            string[] foodsString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string foodString in foodsString)
            {
                Match foodMatch = foodRegex.Match(foodString);
                GroupCollection foodGroups = foodMatch.Groups;

                string ingredientsString = foodGroups[1].Value;
                string allergensString = foodGroups[2].Value;

                List<string> ingredients = ingredientsString.Split(" ").ToList();
                List<string> allergens = allergensString.Split(", ").ToList();

                foods.Add(new Food
                {
                    Ingredients = ingredients,
                    Allergens = allergens
                });
            }

            return foods;
        }
    }
}
