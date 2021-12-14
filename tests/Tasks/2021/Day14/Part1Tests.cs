using System;
using App.Tasks.Year2021.Day14;
using Xunit;

namespace Tests.Tasks.Year2021.Day14
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_PolymerTemplateAndPairInsertionRulesExample_DifferenceBetweenMostAndLeastCommonElementAfterTenStepsEquals()
        {
            string polymerTemplateAndPairInsertionRules = "NNCB"
                + Environment.NewLine
                + $"{Environment.NewLine}CH -> B"
                + $"{Environment.NewLine}HH -> N"
                + $"{Environment.NewLine}CB -> H"
                + $"{Environment.NewLine}NH -> C"
                + $"{Environment.NewLine}HB -> C"
                + $"{Environment.NewLine}HC -> B"
                + $"{Environment.NewLine}HN -> C"
                + $"{Environment.NewLine}NN -> C"
                + $"{Environment.NewLine}BH -> H"
                + $"{Environment.NewLine}NC -> B"
                + $"{Environment.NewLine}NB -> B"
                + $"{Environment.NewLine}BN -> B"
                + $"{Environment.NewLine}BB -> N"
                + $"{Environment.NewLine}BC -> B"
                + $"{Environment.NewLine}CC -> N"
                + $"{Environment.NewLine}CN -> C";

            Assert.Equal(1588, task.Solution(polymerTemplateAndPairInsertionRules));
        }
    }
}
