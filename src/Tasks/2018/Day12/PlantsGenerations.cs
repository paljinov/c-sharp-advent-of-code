using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2018.Day12
{
    public class PlantsGenerations
    {
        private const char PLANT = '#';

        private const char NO_PLANT = '.';

        public int CalculateNumbersSumOfAllPotsWhichContainPlantAfterGenerations(
            string pots,
            Dictionary<string, char> spreadNotes,
            int generations
        )
        {
            Dictionary<int, char> currentGeneration = new Dictionary<int, char>();

            int from = -generations * 4 - 2;
            int to = pots.Length + generations * 4 + 2;

            for (int i = from; i < 0; i++)
            {
                currentGeneration.Add(i, NO_PLANT);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                currentGeneration.Add(i, pots[i]);
            }
            for (int i = pots.Length; i < to; i++)
            {
                currentGeneration.Add(i, NO_PLANT);
            }

            for (int generation = 1; generation <= generations; generation++)
            {
                Dictionary<int, char> nextGeneration = currentGeneration.ToDictionary(p => p.Key, p => p.Value);

                for (int i = from + 2; i < to - 2; i++)
                {
                    StringBuilder adjacentPotsSb = new StringBuilder();
                    for (int j = i - 2; j <= i + 2; j++)
                    {
                        adjacentPotsSb.Append(currentGeneration[j]);
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

                currentGeneration = nextGeneration.ToDictionary(p => p.Key, p => p.Value);
            }

            int numbersSumOfAllPotsWhichContainPlantAfterGenerations = 0;
            foreach (KeyValuePair<int, char> pot in currentGeneration)
            {
                if (pot.Value == PLANT)
                {
                    numbersSumOfAllPotsWhichContainPlantAfterGenerations += pot.Key;
                }
            }

            return numbersSumOfAllPotsWhichContainPlantAfterGenerations;
        }
    }
}
