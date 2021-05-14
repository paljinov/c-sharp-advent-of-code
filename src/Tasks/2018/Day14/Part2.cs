/*
--- Part Two ---

As it turns out, you got the Elves' plan backwards. They actually want to know
how many recipes appear on the scoreboard to the left of the first recipes whose
scores are the digits from your puzzle input.

- 51589 first appears after 9 recipes.
- 01245 first appears after 5 recipes.
- 92510 first appears after 18 recipes.
- 59414 first appears after 2018 recipes.

How many recipes appear on the scoreboard to the left of the score sequence in
your puzzle input?
*/

namespace App.Tasks.Year2018.Day14
{
    public class Part2 : ITask<int>
    {
        private readonly RecipesRepository recipesRepository;

        private readonly Scores scores;

        public Part2()
        {
            recipesRepository = new RecipesRepository();
            scores = new Scores();
        }
        public int Solution(string input)
        {
            int[] recipesScoresSequence = recipesRepository.GetRecipesScoresSequence(input);
            int recipesWhichAppearToTheLeftOfTheScoreSequence = scores
                .CountRecipesWhichAppearToTheLeftOfTheScoreSequence(recipesScoresSequence);

            return recipesWhichAppearToTheLeftOfTheScoreSequence;
        }
    }
}
