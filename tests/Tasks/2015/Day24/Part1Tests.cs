using System;
using App.Tasks.Year2015.Day24;
using Xunit;

namespace Tests.Tasks.Year2015.Day24
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_PackageWeightsExample_FirstGroupQuantumEntanglementForThreeGroupsConfigurationEquals()
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

            Assert.Equal(99, task.Solution(packageWeights));
        }
    }
}
