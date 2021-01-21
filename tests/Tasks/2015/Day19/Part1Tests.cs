using System;
using App.Tasks.Year2015.Day19;
using Xunit;

namespace Tests.Tasks.Year2015.Day19
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstInputMoleculeAndReplacementsExample_CreatedDistinctMoleculesEquals()
        {
            string inputMoleculeAndReplacements = "H => HO"
                + $"{Environment.NewLine}H => OH"
                + $"{Environment.NewLine}O => HH"
                + Environment.NewLine
                + $"{Environment.NewLine}HOH";

            Assert.Equal(4, task.Solution(inputMoleculeAndReplacements));
        }

        [Fact]
        public void Solution_SecondInputMoleculeAndReplacementsExample_CreatedDistinctMoleculesEquals()
        {
            string inputMoleculeAndReplacements = "H => HO"
                + $"{Environment.NewLine}H => OH"
                + $"{Environment.NewLine}O => HH"
                + Environment.NewLine
                + $"{Environment.NewLine}HOHOHO";

            Assert.Equal(7, task.Solution(inputMoleculeAndReplacements));
        }
    }
}
