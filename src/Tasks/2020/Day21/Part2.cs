/*
--- Part Two ---

Now that you've isolated the inert ingredients, you should have enough
information to figure out which ingredient contains which allergen.

In the above example:

mxmxvkd contains dairy.
sqjhc contains fish.
fvjkl contains soy.

Arrange the ingredients alphabetically by their allergen and separate them by
commas to produce your canonical dangerous ingredient list. (There should not be
any spaces in your canonical dangerous ingredient list.) In the above example,
this would be mxmxvkd,sqjhc,fvjkl.

Time to stock your raft with supplies. What is your canonical dangerous
ingredient list?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2020.Day21
{
    public class Part2 : ITask<string>
    {
        private readonly FoodsRepository foodsRepository;

        private readonly Ingredients ingredients;

        public Part2()
        {
            foodsRepository = new FoodsRepository();
            ingredients = new Ingredients();
        }

        public string Solution(string input)
        {
            List<Food> foods = foodsRepository.GetFoods(input);
            string dangerousIngredients = ingredients.GetCommaSeparatedDangerousIngredientsSortedByAllergen(foods);

            return dangerousIngredients;
        }
    }
}
