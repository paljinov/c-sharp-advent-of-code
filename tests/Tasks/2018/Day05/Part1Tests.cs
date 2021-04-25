using App.Tasks.Year2018.Day5;
using Xunit;

namespace Tests.Tasks.Year2018.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_PolymerUnitsExample_RemainingUnitsAfterFullyReactingThePolymerEquals()
        {
            string polymerUnits = "dabAcCaCBAcCcaDA";
            Assert.Equal(10, task.Solution(polymerUnits));
        }
    }
}
