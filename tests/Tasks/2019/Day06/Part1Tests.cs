using System;
using App.Tasks.Year2019.Day6;
using Xunit;

namespace Tests.Tasks.Year2019.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_OrbitsExample_TotalNumberOfDirectAndIndirectOrbitsCountEquals()
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
                + $"{Environment.NewLine}K)L";

            Assert.Equal(42, task.Solution(orbits));
        }
    }
}
