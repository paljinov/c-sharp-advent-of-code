using System;
using System.Reflection;
using App.Tasks.Year2015.Day18;
using Xunit;

namespace Tests.Tasks.Year2015.Day18
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            LightsGrid lightsGrid = new LightsGrid();
            lightsGrid.GetType()
                .GetField("steps", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(lightsGrid, 4);

            task.GetType()
                .GetField("lightsGrid", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, lightsGrid);
        }

        [Fact]
        public void Solution_InitialLightsConfigurationExample_TurnedOnLightsAfterFourStepsAnimationEquals()
        {
            string initialLightsConfiguration = ".#.#.#"
                + $"{Environment.NewLine}...##."
                + $"{Environment.NewLine}#....#"
                + $"{Environment.NewLine}..#..."
                + $"{Environment.NewLine}#.#..#"
                + $"{Environment.NewLine}####..";

            Assert.Equal(4, task.Solution(initialLightsConfiguration));
        }
    }
}
