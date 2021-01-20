using System;
using System.Reflection;
using App.Tasks.Year2015.Day18;
using Xunit;

namespace Tests.Tasks.Year2015.Day18
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            LightsGrid lightsGrid = new LightsGrid();
            lightsGrid.GetType()
                .GetField("steps", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(lightsGrid, 5);

            task.GetType()
                .GetField("lightsGrid", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, lightsGrid);
        }

        [Fact]
        public void Solution_InitialLightsConfigurationExample_TurnedOnLightsAfterFiveStepsAnimationEquals()
        {
            string initialLightsConfiguration = "##.#.#"
                + $"{Environment.NewLine}...##."
                + $"{Environment.NewLine}#....#"
                + $"{Environment.NewLine}..#..."
                + $"{Environment.NewLine}#.#..#"
                + $"{Environment.NewLine}####.#";

            Assert.Equal(17, task.Solution(initialLightsConfiguration));
        }
    }
}
