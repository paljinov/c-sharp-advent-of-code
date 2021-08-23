using System.Collections.Generic;

namespace App.Tasks.Year2019.Day14
{
    public class FuelNanofactory
    {
        private const string ORE = "ORE";

        private const string FUEL = "FUEL";

        public int CalculateMinimumAmountOfOreRequiredToProduceExactlyOneFuel(Dictionary<string, Reaction> reactions)
        {
            Dictionary<string, int> materialChemicals = InitializeMaterialChemicals(reactions);

            int minimumAmountOfOre = CalculateMinimumAmountOfOreRequiredToProduceChemical(
                reactions[FUEL], materialChemicals, reactions);

            return minimumAmountOfOre;
        }

        public long CalculateMaximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOre(
            Dictionary<string, Reaction> reactions,
            long totalOre
        )
        {
            Dictionary<string, int> materialChemicals = InitializeMaterialChemicals(reactions);
            List<Dictionary<string, int>> materialChemicalsCycle = new List<Dictionary<string, int>>();

            long usedOreForCycle = 0;
            long producedFuelForCycle = 0;

            // Loop until all material is used without reminder, that makes one full cycle
            while (materialChemicals[FUEL] == 0 || !AreAllMaterialChemicalsUsed(materialChemicals))
            {
                materialChemicalsCycle.Add(new Dictionary<string, int>(materialChemicals));

                long oreRequired = CalculateMinimumAmountOfOreRequiredToProduceChemical(
                    reactions[FUEL], materialChemicals, reactions);

                if (usedOreForCycle + oreRequired <= totalOre)
                {
                    usedOreForCycle += oreRequired;
                    producedFuelForCycle++;
                }
                else
                {
                    return producedFuelForCycle;
                }
            }

            long totalFullCycles = totalOre / usedOreForCycle;
            // Remaining ore outside full cycles
            long remainingOre = totalOre - totalFullCycles * usedOreForCycle;

            long maximumAmountOfFuel = totalFullCycles * producedFuelForCycle;

            // Use remaining ore
            while (remainingOre >= 0)
            {
                int usedOre = CalculateMinimumAmountOfOreRequiredToProduceChemical(
                    reactions[FUEL], materialChemicals, reactions);

                remainingOre -= usedOre;
                if (remainingOre >= 0)
                {
                    maximumAmountOfFuel++;
                }
            }

            return maximumAmountOfFuel;
        }

        private int CalculateMinimumAmountOfOreRequiredToProduceChemical(
            Reaction currentReaction,
            Dictionary<string, int> materialChemicals,
            Dictionary<string, Reaction> reactions
        )
        {
            int minimumAmountOfOre = 0;

            foreach (Chemical chemical in currentReaction.InputChemicals)
            {
                if (chemical.Name == ORE)
                {
                    minimumAmountOfOre += chemical.Amount;
                }
                else
                {
                    // If there is not enough of this chemical in materials stockpile it needs to be produced
                    while (materialChemicals[chemical.Name] < chemical.Amount)
                    {
                        Reaction nextReaction = reactions[chemical.Name];
                        minimumAmountOfOre += CalculateMinimumAmountOfOreRequiredToProduceChemical(
                            nextReaction, materialChemicals, reactions);
                    }

                    // Subtract chemical from material stockpile
                    materialChemicals[chemical.Name] -= chemical.Amount;
                }
            }

            // Add produced chemical to material stockpile
            materialChemicals[currentReaction.OutputChemical.Name] += currentReaction.OutputChemical.Amount;

            return minimumAmountOfOre;
        }

        private Dictionary<string, int> InitializeMaterialChemicals(Dictionary<string, Reaction> reactions)
        {
            Dictionary<string, int> materialChemicals = new Dictionary<string, int>();
            foreach (KeyValuePair<string, Reaction> reaction in reactions)
            {
                materialChemicals[reaction.Value.OutputChemical.Name] = 0;
            }

            return materialChemicals;
        }

        private bool AreAllMaterialChemicalsUsed(Dictionary<string, int> materialChemicals)
        {
            foreach (KeyValuePair<string, int> chemical in materialChemicals)
            {
                if (chemical.Key != FUEL && chemical.Value > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
