namespace App.Tasks.Year2019.Day1
{
    public class FuelCounter
    {
        private const int DIVIDE_BY = 3;

        private const int SUBTRACT = 2;

        public int CalculateSumForAllModulesFuelRequirements(int[] modulesMasses)
        {
            int sumForAllModulesFuelRequirements = 0;

            foreach (int moduleMass in modulesMasses)
            {
                int moduleFuelRequirements = (int)(moduleMass / DIVIDE_BY) - SUBTRACT;
                sumForAllModulesFuelRequirements += moduleFuelRequirements;
            }

            return sumForAllModulesFuelRequirements;
        }
    }
}
