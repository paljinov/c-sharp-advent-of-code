/*
--- Part Two ---

In all the commotion, you realize that you forgot to seat yourself. At this
point, you're pretty apathetic toward the whole thing, and your happiness
wouldn't really go up or down regardless of who you sit next to. You assume
everyone else would be just as ambivalent about sitting next to you, too.

So, add yourself to the list, and give all happiness relationships that involve
you a score of 0.

What is the total change in happiness for the optimal seating arrangement that
actually includes yourself?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day13
{
     class Part2 : ITask<int>
    {
        private readonly SittingCombinations sittingCombinations;

        public Part2()
        {
            sittingCombinations = new SittingCombinations();
        }

        public int Solution(string input)
        {
            Dictionary<string, int> sittingsHappiness = this.sittingCombinations.Parse(input);
            HashSet<string> persons = this.sittingCombinations.GetPersons(sittingsHappiness);
            foreach (string person in persons)
            {
                sittingsHappiness.Add($"Me->{person}", 0);
                sittingsHappiness.Add($"{person}->Me", 0);
            }

            List<Dictionary<string, int>> sittingCombinations =
                this.sittingCombinations.GetSittingCombinations(sittingsHappiness);

            int optimalTotalChangeInHappiness =
                this.sittingCombinations.CalculateOptimalTotalChangeInHappiness(sittingCombinations);

            return optimalTotalChangeInHappiness;
        }
    }
}
