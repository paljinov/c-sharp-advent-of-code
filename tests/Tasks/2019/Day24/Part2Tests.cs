using System;
using System.Reflection;
using App.Tasks.Year2019.Day24;
using Xunit;

namespace Tests.Tasks.Year2019.Day24
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            task.GetType()
               .GetField("totalMinutes", BindingFlags.Instance | BindingFlags.NonPublic)
               .SetValue(task, 10);
        }

        [Fact]
        public void Solution_AreaGridExample_BugsCountWhichArePresentAfterTwoHundredMinutesEquals()
        {
            string areaGrid = "....#"
                + $"{Environment.NewLine}#..#."
                + $"{Environment.NewLine}#..##"
                + $"{Environment.NewLine}..#.."
                + $"{Environment.NewLine}#....";

            Assert.Equal(99, task.Solution(areaGrid));
        }
    }
}
