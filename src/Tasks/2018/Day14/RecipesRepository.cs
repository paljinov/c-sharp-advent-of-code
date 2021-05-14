namespace App.Tasks.Year2018.Day14
{
    public class RecipesRepository
    {
        public int GetNumberOfRecipesRepository(string input)
        {
            int numberOfRecipes = int.Parse(input);
            return numberOfRecipes;
        }

        public int[] GetRecipesScoresSequence(string input)
        {
            int[] recipesScoresSequence = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                recipesScoresSequence[i] = (int)char.GetNumericValue(input[i]);
            }

            return recipesScoresSequence;
        }
    }
}
