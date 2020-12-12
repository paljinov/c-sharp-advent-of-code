using System;
using App.Tasks.Year2020.Day12;
using Xunit;

namespace Tests.Tasks.Year2020.Day12
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ShipNavigationInstructionsExample_ManhattanDistanceBetweenStartAndEndPositionEquals()
        {
            string shipNavigationInstructions = "F10"
                + $"{Environment.NewLine}N3"
                + $"{Environment.NewLine}F7"
                + $"{Environment.NewLine}R90"
                + $"{Environment.NewLine}F11";

            Assert.Equal(25, task.Solution(shipNavigationInstructions));
        }
    }
}
