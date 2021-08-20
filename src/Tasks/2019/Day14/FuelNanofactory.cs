using System.Collections.Generic;

namespace App.Tasks.Year2019.Day14
{
    public class FuelNanofactory
    {
        private const string ORE = "ORE";

        private const string FUEL = "FUEL";

        public int CalculateMinimumAmountOfOreRequiredToProduceExactlyOneFuel(List<Reaction> reactions)
        {
            Reaction currentReaction = FindReactionWhereOutputIsChemical(FUEL, reactions);
            Dictionary<string, int> materialChemicals = InitializeMaterialChemicals(reactions);

            int totalOre = DoCalculateMinimumAmountOfOreRequiredToProduceExactlyOneFuel(
                currentReaction, materialChemicals, reactions);

            return totalOre;
        }

        private int DoCalculateMinimumAmountOfOreRequiredToProduceExactlyOneFuel(
            Reaction currentReaction,
            Dictionary<string, int> materialChemicals,
            List<Reaction> reactions
        )
        {
            int totalOre = 0;

            foreach (Chemical chemical in currentReaction.InputChemicals)
            {
                if (chemical.Name == ORE)
                {
                    totalOre += chemical.Amount;
                }
                else
                {
                    // If there is not enough of this chemical in materials stockpile it needs to be produced
                    while (materialChemicals[chemical.Name] < chemical.Amount)
                    {
                        Reaction nextReaction = FindReactionWhereOutputIsChemical(chemical.Name, reactions);
                        totalOre += DoCalculateMinimumAmountOfOreRequiredToProduceExactlyOneFuel(
                            nextReaction, materialChemicals, reactions);
                    }

                    materialChemicals[chemical.Name] -= chemical.Amount;
                }
            }

            materialChemicals[currentReaction.OutputChemical.Name] += currentReaction.OutputChemical.Amount;

            return totalOre;
        }

        private Dictionary<string, int> InitializeMaterialChemicals(List<Reaction> reactions)
        {
            Dictionary<string, int> materialChemicals = new Dictionary<string, int>();
            foreach (Reaction reaction in reactions)
            {
                materialChemicals[reaction.OutputChemical.Name] = 0;
            }

            return materialChemicals;
        }

        private Reaction FindReactionWhereOutputIsChemical(string chemical, List<Reaction> reactions)
        {
            foreach (Reaction reaction in reactions)
            {
                if (reaction.OutputChemical.Name == chemical)
                {
                    return reaction;
                }
            }

            return null;
        }
    }
}
