using System;
using App.Tasks.Year2020.Day12;
using Xunit;

namespace Tests.Tasks.Year2020.Day12
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ShipNavigationInstructionsExample_ManhattanDistanceBetweenStartAndEndPositionByMovingWaypointEquals()
        {
            string shipNavigationInstructions = "F10"
                + $"{Environment.NewLine}N3"
                + $"{Environment.NewLine}F7"
                + $"{Environment.NewLine}R90"
                + $"{Environment.NewLine}F11";

            Assert.Equal(286, task.Solution(shipNavigationInstructions));
        }
    }
}
