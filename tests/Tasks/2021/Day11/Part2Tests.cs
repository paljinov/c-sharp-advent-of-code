using System;
using App.Tasks.Year2021.Day11;
using Xunit;

namespace Tests.Tasks.Year2021.Day11
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_OctopusesEnergyLevelsExample_FirstStepDuringWhichAllOctopusesFlashEquals()
        {
            string octopusesEnergyLevels = "5483143223"
                + $"{Environment.NewLine}2745854711"
                + $"{Environment.NewLine}5264556173"
                + $"{Environment.NewLine}6141336146"
                + $"{Environment.NewLine}6357385478"
                + $"{Environment.NewLine}4167524645"
                + $"{Environment.NewLine}2176841721"
                + $"{Environment.NewLine}6882881134"
                + $"{Environment.NewLine}4846848554"
                + $"{Environment.NewLine}5283751526";

            Assert.Equal(195, task.Solution(octopusesEnergyLevels));
        }
    }
}
