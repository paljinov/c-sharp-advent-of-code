using System;
using App.Tasks.Year2018.Day17;
using Xunit;

namespace Tests.Tasks.Year2018.Day17
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ClayVeinsExample_LeftWaterTilesCountEquals()
        {
            string clayVeins = "x=495, y=2..7"
                + $"{Environment.NewLine}y=7, x=495..501"
                + $"{Environment.NewLine}x=501, y=3..7"
                + $"{Environment.NewLine}x=498, y=2..4"
                + $"{Environment.NewLine}x=506, y=1..2"
                + $"{Environment.NewLine}x=498, y=10..13"
                + $"{Environment.NewLine}x=504, y=10..13"
                + $"{Environment.NewLine}y=13, x=498..504";

            Assert.Equal(29, task.Solution(clayVeins));
        }
    }
}
