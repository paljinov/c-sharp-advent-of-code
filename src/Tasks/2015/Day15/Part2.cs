/*
--- Part Two ---

Your cookie recipe becomes wildly popular! Someone asks if you can make another
recipe that has exactly 500 calories per cookie (so they can use it as a meal
replacement). Keep the rest of your award-winning process the same (100
teaspoons, same ingredients, same scoring system).

For example, given the ingredients above, if you had instead selected 40
teaspoons of butterscotch and 60 teaspoons of cinnamon (which still adds to
100), the total calorie count would be 40*8 + 60*3 = 500. The total score would
go down, though: only 57600000, the best you can do in such trying
circumstances.

Given the ingredients in your kitchen and their properties, what is the total
score of the highest-scoring cookie you can make with a calorie total of 500?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day15
{
    class Part2 : ITask<int>
    {
        private readonly Cookie cookie;

        private readonly IngredientsRepository ingredientsRepository;

        public Part2()
        {
            cookie = new Cookie();
            ingredientsRepository = new IngredientsRepository();
        }

        public int Solution(string input)
        {
            Dictionary<string, Ingredient> ingredients = ingredientsRepository.ParseInput(input);
            int highestScoringCookie = cookie.CalculateHighestScoringCookie(ingredients, true);

            return highestScoringCookie;
        }
    }
}
