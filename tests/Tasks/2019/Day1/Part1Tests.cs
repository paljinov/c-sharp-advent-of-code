using App.Tasks.Year2019.Day1;
using Xunit;

namespace Tests.Tasks.Year2019.Day1
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("12", 2)]
        [InlineData("14", 2)]
        [InlineData("1969", 654)]
        [InlineData("100756", 33583)]
        public void Solution_ModulesMassesExample_SumOfTheFuelRequirementsForAllOfTheModulesEquals(
            string modulesMasses,
            int sumOfTheFuelRequirementsForAllOfTheModules
        )
        {
            Assert.Equal(sumOfTheFuelRequirementsForAllOfTheModules, task.Solution(modulesMasses));
        }
    }
}
