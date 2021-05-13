using System.Collections.Generic;
using System.Text;

namespace App.Tasks.Year2018.Day12
{
    public class PlantsGenerations
    {
        private const char PLANT = '#';

        private const char NO_PLANT = '.';

        private const int SPREAD_GROWTH_REPETITIONS_LIMIT = 10;

        public long CalculateSumOfTheNumbersOfAllPotsWhichContainPlantAfterGenerations(
            string pots,
            Dictionary<string, char> spreadNotes,
            long generations
        )
        {
            Dictionary<int, char> currentGeneration = new Dictionary<int, char>();
            for (int i = 0; i < pots.Length; i++)
            {
                currentGeneration.Add(i, pots[i]);
            }

            int previousSpreadGrowth = 0;
            int spreadGrowthRepetitions = 0;
            long generation;

            for (generation = 1; generation <= generations; generation++)
            {
                Dictionary<int, char> nextGeneration = new Dictionary<int, char>(currentGeneration);
                int sumOfTheNumbersOfAllPots = CalculateSumOfTheNumbersOfAllPotsForGeneration(currentGeneration);

                for (int i = -2; i < currentGeneration.Count + 2; i++)
                {
                    StringBuilder adjacentPotsSb = new StringBuilder();
                    for (int j = i - 2; j <= i + 2; j++)
                    {
                        if (currentGeneration.ContainsKey(j))
                        {
                            adjacentPotsSb.Append(currentGeneration[j]);
                        }
                        else
                        {
                            adjacentPotsSb.Append(NO_PLANT);
                        }
                    }

                    string adjacentPots = adjacentPotsSb.ToString();
                    if (spreadNotes.ContainsKey(adjacentPots))
                    {
                        nextGeneration[i] = spreadNotes[adjacentPots];
                    }
                    else
                    {
                        nextGeneration[i] = NO_PLANT;
                    }
                }

                int spreadGrowth = CalculateSumOfTheNumbersOfAllPotsForGeneration(nextGeneration)
                    - sumOfTheNumbersOfAllPots;

                // Is spread growth is the same as in the last generation
                if (spreadGrowth == previousSpreadGrowth)
                {
                    spreadGrowthRepetitions++;
                }
                else
                {
                    spreadGrowthRepetitions = 0;
                }

                // After spread growth is repeated many times it is continuous growth
                if (spreadGrowthRepetitions == SPREAD_GROWTH_REPETITIONS_LIMIT)
                {
                    break;
                }

                previousSpreadGrowth = spreadGrowth;

                currentGeneration = new Dictionary<int, char>(nextGeneration);
            }

            long sumOfTheNumbersOfAllPotsWhichContainPlantAfterGenerations =
                CalculateSumOfTheNumbersOfAllPotsForGeneration(currentGeneration);

            sumOfTheNumbersOfAllPotsWhichContainPlantAfterGenerations +=
                (generations - (generation - 1)) * previousSpreadGrowth;

            return sumOfTheNumbersOfAllPotsWhichContainPlantAfterGenerations;
        }

        private int CalculateSumOfTheNumbersOfAllPotsForGeneration(Dictionary<int, char> generation)
        {
            int sumOfTheNumbersOfAllPots = 0;
            foreach (KeyValuePair<int, char> pot in generation)
            {
                if (pot.Value == PLANT)
                {
                    sumOfTheNumbersOfAllPots += pot.Key;
                }
            }

            return sumOfTheNumbersOfAllPots;
        }
    }
}
