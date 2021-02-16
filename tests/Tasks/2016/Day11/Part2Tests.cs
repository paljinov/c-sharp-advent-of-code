using System;
using System.Reflection;
using App.Tasks.Year2016.Day11;
using Xunit;

namespace Tests.Tasks.Year2016.Day11
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
            task.GetType()
              .GetField("additionalPairsOnMinFloor", BindingFlags.Instance | BindingFlags.NonPublic)
              .SetValue(task, new string[] { "elerium" });
        }

        [Fact]
        public void Solution_FloorsObjectsArrangementExample_MinimumNumberOfStepsToBringAllObjectsToLastFloorEquals()
        {
            string floorsObjectsArrangement = "The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip."
                + $"{Environment.NewLine}The second floor contains a hydrogen generator."
                + $"{Environment.NewLine}The third floor contains a lithium generator."
                + $"{Environment.NewLine}The fourth floor contains nothing relevant.";

            Assert.Equal(21, task.Solution(floorsObjectsArrangement));
        }
    }
}
