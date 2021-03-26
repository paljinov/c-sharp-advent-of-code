using System;
using App.Tasks.Year2017.Day15;
using Xunit;

namespace Tests.Tasks.Year2017.Day15
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_GeneratorsStartingValuesExample_JudgeFinalCountEquals()
        {
            string generatorsStartingValues = $"{Environment.NewLine}Generator A starts with 65"
                + $"{Environment.NewLine}Generator B starts with 8921";

            Assert.Equal(309, task.Solution(generatorsStartingValues));
        }
    }
}
