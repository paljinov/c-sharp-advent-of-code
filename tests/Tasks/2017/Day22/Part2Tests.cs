using System;
using System.Reflection;
using App.Tasks.Year2017.Day22;
using Xunit;

namespace Tests.Tasks.Year2017.Day22
{
    public class Part2Tests
    {
        private readonly Part2 task;

        private readonly string map;

        public Part2Tests()
        {
            task = new Part2();
            map = "..#"
               + $"{Environment.NewLine}#.."
               + $"{Environment.NewLine}...";
        }

        [Fact]
        public void Solution_MapExample_BurstsThatCausedAnInfectionForEvolvedVirusAfterOneHundredBurstsOfActivityEquals()
        {
            typeof(Part2)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 100);

            Assert.Equal(26, task.Solution(map));
        }

        [Fact]
        public void Solution_MapExample_BurstsThatCausedAnInfectionForEvolvedVirusAfterTenMillionBurstsOfActivityEquals()
        {
            typeof(Part2)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 10000000);

            Assert.Equal(2511944, task.Solution(map));
        }
    }
}
