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
            typeof(Part1)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 7);

            Assert.Equal(5, task.Solution(map));
        }

        [Fact]
        public void Solution_MapExample_BurstsThatCausedAnInfectionAfterSeventyBurstsOfActivityEquals()
        {
            typeof(Part1)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 70);

            Assert.Equal(41, task.Solution(map));
        }

        [Fact]
        public void Solution_MapExample_BurstsThatCausedAnInfectionAfterTenThousandBurstsOfActivityEquals()
        {
            typeof(Part1)
                .GetField("totalBurstsOfActivity", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 10000);

            Assert.Equal(5587, task.Solution(map));
        }
    }
}
