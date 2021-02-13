using System;
using App.Tasks.Year2016.Day11;
using Xunit;

namespace Tests.Tasks.Year2016.Day11
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FloorsObjectsArrangementExample_MinimumNumberOfStepsToBringAllObjectsToLastFloorEquals()
        {
            string floorsObjectsArrangement = "The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip."
                + $"{Environment.NewLine}The second floor contains a hydrogen generator."
                + $"{Environment.NewLine}The third floor contains a lithium generator."
                + $"{Environment.NewLine}The fourth floor contains nothing relevant.";

            Assert.Equal(11, task.Solution(floorsObjectsArrangement));
        }
    }
}
