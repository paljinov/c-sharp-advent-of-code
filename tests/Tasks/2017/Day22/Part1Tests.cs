using System;
using System.Reflection;
using App.Tasks.Year2017.Day22;
using Xunit;

namespace Tests.Tasks.Year2017.Day22
{
    public class Part1Tests
    {
        private readonly Part1 task;

        private readonly string map;

        public Part1Tests()
        {
            task = new Part1();
            map = "..#"
               + $"{Environment.NewLine}#.."
               + $"{Environment.NewLine}...";
        }

        [Fact]
        public void Solution_MapExample_BurstsThatCausedAnInfectionAfterSevenBurstsOfActivityEquals()
        {
            VirusCarrier virusCarrier = new VirusCarrier();
            typeof(VirusCarrier)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(virusCarrier, 7);
            typeof(Part1)
                .GetField("virusCarrier", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, virusCarrier);

            Assert.Equal(5, task.Solution(map));
        }

        [Fact]
        public void Solution_MapExample_BurstsThatCausedAnInfectionAfterSeventyBurstsOfActivityEquals()
        {
            VirusCarrier virusCarrier = new VirusCarrier();
            typeof(VirusCarrier)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(virusCarrier, 70);
            typeof(Part1)
                .GetField("virusCarrier", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, virusCarrier);

            Assert.Equal(41, task.Solution(map));
        }

        [Fact]
        public void Solution_MapExample_BurstsThatCausedAnInfectionAfterTenThousandBurstsOfActivityEquals()
        {
            VirusCarrier virusCarrier = new VirusCarrier();
            typeof(VirusCarrier)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(virusCarrier, 10000);
            typeof(Part1)
                .GetField("virusCarrier", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, virusCarrier);

            Assert.Equal(5587, task.Solution(map));
        }
    }
}
