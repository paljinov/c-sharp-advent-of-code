using System;
using App.Tasks.Year2015.Day19;
using Xunit;

namespace Tests.Tasks.Year2015.Day19
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstInputMoleculeAndReplacementsExample_FewestStepsFromElectronToMedicineMoleculeEquals()
        {
            string inputMoleculeAndReplacements = "e => H"
                + $"{Environment.NewLine}e => O"
                + $"{Environment.NewLine}H => HO"
                + $"{Environment.NewLine}H => OH"
                + $"{Environment.NewLine}O => HH"
                + Environment.NewLine
                + $"{Environment.NewLine}HOH";

            Assert.Equal(3, task.Solution(inputMoleculeAndReplacements));
        }

        [Fact]
        public void Solution_SecondInputMoleculeAndReplacementsExample_FewestStepsFromElectronToMedicineMoleculeEquals()
        {
            string inputMoleculeAndReplacements = "e => H"
                + $"{Environment.NewLine}e => O"
                + $"{Environment.NewLine}H => HO"
                + $"{Environment.NewLine}H => OH"
                + $"{Environment.NewLine}O => HH"
                + Environment.NewLine
                + $"{Environment.NewLine}HOHOHO";

            Assert.Equal(6, task.Solution(inputMoleculeAndReplacements));
        }
    }
}
