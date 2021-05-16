namespace App.Tasks.Year2019.Day1
{
    public class FuelCounter
    {
        private const int DIVIDE_BY = 3;

        private const int SUBTRACT = 2;

        public int CalculateSumOfTheFuelRequirementsForAllOfTheModules(
            int[] modulesMasses,
            bool accountMassOfAddedFuel = false
        )
        {
            int sumOfTheFuelRequirementsForAllOfTheModules = 0;

            foreach (int moduleMass in modulesMasses)
            {
                int moduleFuelRequirements = CalculateModuleFuelRequirements(moduleMass, accountMassOfAddedFuel);
                sumOfTheFuelRequirementsForAllOfTheModules += moduleFuelRequirements;
            }

            return sumOfTheFuelRequirementsForAllOfTheModules;
        }

        private int CalculateModuleFuelRequirements(int moduleMass, bool accountMassOfAddedFuel)
        {
            int moduleFuelRequirements = (int)(moduleMass / DIVIDE_BY) - SUBTRACT;
            if (moduleFuelRequirements < 0)
            {
                moduleFuelRequirements = 0;
            }
            else if (accountMassOfAddedFuel)
            {
                moduleFuelRequirements += CalculateModuleFuelRequirements(moduleFuelRequirements, true);
            }

            return moduleFuelRequirements;
        }
    }
}
