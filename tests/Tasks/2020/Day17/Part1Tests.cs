using System;
using App.Tasks.Year2020.Day17;
using Xunit;

namespace Tests.Tasks.Year2020.Day17
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InitialPocket2DSliceExample_ActiveCubesFor3DPocketAfter6CyclesEquals()
        {
            string initialPocket2DSlice = ".#."
                + $"{Environment.NewLine}..#"
                + $"{Environment.NewLine}###";

            Assert.Equal(112, task.Solution(initialPocket2DSlice));
        }
    }
}
