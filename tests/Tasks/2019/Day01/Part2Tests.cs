using App.Tasks.Year2019.Day1;
using Xunit;

namespace Tests.Tasks.Year2019.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("12", 2)]
        [InlineData("14", 2)]
        [InlineData("1969", 966)]
        [InlineData("100756", 50346)]
        public void Solution_ModulesMassesExample_SumOfTheFuelRequirementsForAllOfTheModulesWhenTakingIntoAccountTheMassOfAddedFuelEquals(
            string modulesMasses,
            int sumOfTheFuelRequirementsForAllOfTheModulesWhenTakingIntoAccountTheMassOfAddedFuel
        )
        {
            Assert.Equal(
                sumOfTheFuelRequirementsForAllOfTheModulesWhenTakingIntoAccountTheMassOfAddedFuel,
                task.Solution(modulesMasses)
            );
        }
    }
}
