using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day14
{
    public class FuelNanofactory
    {
        private const string ORE = "ORE";

        private const string FUEL = "FUEL";

        public int CalculateMinimumAmountOfOreRequiredToProduceExactlyOneFuel(Dictionary<string, Reaction> reactions)
        {
            Dictionary<string, int> materialChemicals = InitializeMaterialChemicals(reactions);

            int minimumAmountOfOre = CalculateDiscreteAmountOfOreRequiredToProduceChemical(
                reactions[FUEL], materialChemicals, reactions);

            return minimumAmountOfOre;
        }

        public long CalculateMaximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOre(
            Dictionary<string, Reaction> reactions,
            long totalOre
        )
        {
            double averageAmountOfOre = CalculateAverageAmountOfOreRequiredToProduceChemical(
                reactions[FUEL], reactions);

            long maximumAmountOfFuel = (long)(totalOre / averageAmountOfOre);

            return maximumAmountOfFuel;
        }

        private int CalculateDiscreteAmountOfOreRequiredToProduceChemical(
            Reaction currentReaction,
            Dictionary<string, int> materialChemicals,
            Dictionary<string, Reaction> reactions
        )
        {
            int discreteAmountOfOre = 0;

            foreach (Chemical chemical in currentReaction.InputChemicals)
            {
                if (chemical.Name == ORE)
                {
                    discreteAmountOfOre += chemical.Amount;
                }
                else
                {
                    // If there is not enough of this chemical in materials stockpile it needs to be produced
                    while (materialChemicals[chemical.Name] < chemical.Amount)
                    {
                        Reaction nextReaction = reactions[chemical.Name];
                        discreteAmountOfOre += CalculateDiscreteAmountOfOreRequiredToProduceChemical(
                            nextReaction, materialChemicals, reactions);
                    }

                    // Subtract chemical from material stockpile
                    materialChemicals[chemical.Name] -= chemical.Amount;
                }
            }

            // Add produced chemical to material stockpile
            materialChemicals[currentReaction.OutputChemical.Name] += currentReaction.OutputChemical.Amount;

            return discreteAmountOfOre;
        }

        private double CalculateAverageAmountOfOreRequiredToProduceChemical(
            Reaction currentReaction,
            Dictionary<string, Reaction> reactions
        )
        {
            double averageAmountOfOre = 0;

            foreach (Chemical chemical in currentReaction.InputChemicals)
            {
                if (chemical.Name == ORE)
                {
                    averageAmountOfOre += chemical.Amount;
                }
                else
                {
                    Reaction nextReaction = reactions[chemical.Name];
                    averageAmountOfOre += ((double)chemical.Amount / nextReaction.OutputChemical.Amount)
                        * CalculateAverageAmountOfOreRequiredToProduceChemical(nextReaction, reactions);
                }
            }

            return averageAmountOfOre;
        }

        private Dictionary<string, int> InitializeMaterialChemicals(Dictionary<string, Reaction> reactions)
        {
            Dictionary<string, int> materialChemicals = new Dictionary<string, int>();
            foreach (KeyValuePair<string, Reaction> reaction in reactions)
            {
                materialChemicals[reaction.Key] = 0;
            }

            return materialChemicals;
        }
    }
}
