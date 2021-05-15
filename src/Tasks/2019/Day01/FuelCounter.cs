namespace App.Tasks.Year2019.Day1
{
    public class FuelCounter
    {
        private const int DIVIDE_BY = 3;

        private const int SUBTRACT = 2;

        public int CalculateSumOfTheFuelRequirementsForAllOfTheModules(int[] modulesMasses)
        {
            int sumOfTheFuelRequirementsForAllOfTheModules = 0;

            foreach (int moduleMass in modulesMasses)
            {
                int moduleFuelRequirements = (int)(moduleMass / DIVIDE_BY) - SUBTRACT;
                sumOfTheFuelRequirementsForAllOfTheModules += moduleFuelRequirements;
            }

            return sumOfTheFuelRequirementsForAllOfTheModules;
        }
    }
}
