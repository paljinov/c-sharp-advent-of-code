using System;
using App.Tasks.Year2020.Day17;
using Xunit;

namespace Tests.Tasks.Year2020.Day17
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InitialPocket2DSliceExample_ActiveCubesFor4DPocketAfter6CyclesEquals()
        {
            string initialPocket2DSlice = ".#."
                + $"{Environment.NewLine}..#"
                + $"{Environment.NewLine}###";

            Assert.Equal(848, task.Solution(initialPocket2DSlice));
        }
    }
}
