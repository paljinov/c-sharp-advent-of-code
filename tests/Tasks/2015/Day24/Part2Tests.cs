using System;
using App.Tasks.Year2015.Day24;
using Xunit;

namespace Tests.Tasks.Year2015.Day24
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_PackageWeightsExample_FirstGroupQuantumEntanglementForFourGroupsConfigurationEquals()
        {
            string packageWeights = "1"
                + $"{Environment.NewLine}2"
                + $"{Environment.NewLine}3"
                + $"{Environment.NewLine}4"
                + $"{Environment.NewLine}5"
                + $"{Environment.NewLine}7"
                + $"{Environment.NewLine}8"
                + $"{Environment.NewLine}9"
                + $"{Environment.NewLine}10"
                + $"{Environment.NewLine}11";

            Assert.Equal(44, task.Solution(packageWeights));
        }
    }
}
