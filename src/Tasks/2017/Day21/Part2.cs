/*
--- Part Two ---

How many pixels stay on after 18 iterations?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2017.Day21
{
    public class Part2 : ITask<int>
    {
        private readonly int totalIterations = 18;

        private readonly RulesRepository rulesRepository;

        private readonly Grid grid;

        public Part2()
        {
            rulesRepository = new RulesRepository();
            grid = new Grid();
        }

        public int Solution(string input)
        {
            char[,] gridOfPixelsInitialState = rulesRepository.GetGridOfPixelsInitialState();
            Dictionary<char[,], char[,]> rules = rulesRepository.GetRules(input);

            int turnedOnPixels =
                grid.CountTurnedOnPixelsAfterIterations(gridOfPixelsInitialState, rules, totalIterations);

            return turnedOnPixels;
        }
    }
}
