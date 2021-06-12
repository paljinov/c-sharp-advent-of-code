using System;
using App.Tasks.Year2019.Day6;
using Xunit;

namespace Tests.Tasks.Year2019.Day6
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_OrbitsExample_MinimumNumberOfOrbitalTransfersRequiredToReachSantaEquals()
        {
            string orbits = "COM)B"
                + $"{Environment.NewLine}B)C"
                + $"{Environment.NewLine}C)D"
                + $"{Environment.NewLine}D)E"
                + $"{Environment.NewLine}E)F"
                + $"{Environment.NewLine}B)G"
                + $"{Environment.NewLine}G)H"
                + $"{Environment.NewLine}D)I"
                + $"{Environment.NewLine}E)J"
                + $"{Environment.NewLine}J)K"
                + $"{Environment.NewLine}K)L"
                + $"{Environment.NewLine}K)YOU"
                + $"{Environment.NewLine}I)SAN";

            Assert.Equal(4, task.Solution(orbits));
        }
    }
}
