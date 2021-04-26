using App.Tasks.Year2018.Day5;
using Xunit;

namespace Tests.Tasks.Year2018.Day5
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_PolymerUnitsExample_LengthOfShortestPolymerByRemovingAllUnitsOfExactlyOneTypeEquals()
        {
            string polymerUnits = "dabAcCaCBAcCcaDA";
            Assert.Equal(4, task.Solution(polymerUnits));
        }
    }
}
