using System;
using App.Tasks.Year2017.Day15;
using Xunit;

namespace Tests.Tasks.Year2017.Day15
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_GeneratorsStartingValuesExample_JudgeFinalCountEquals()
        {
            string generatorsStartingValues = $"{Environment.NewLine}Generator A starts with 65"
                + $"{Environment.NewLine}Generator B starts with 8921";

            Assert.Equal(588, task.Solution(generatorsStartingValues));
        }
    }
}
