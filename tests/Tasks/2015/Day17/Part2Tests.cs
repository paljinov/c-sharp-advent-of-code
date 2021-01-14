using System;
using System.Reflection;
using App.Tasks.Year2015.Day17;
using Xunit;

namespace Tests.Tasks.Year2015.Day17
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            ContainersCombinations containersCombinations = new ContainersCombinations();
            containersCombinations.GetType()
                .GetField("eggnogLiters", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(containersCombinations, 25);

            task.GetType()
                .GetField("containersCombinations", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, containersCombinations);
        }

        [Fact]
        public void Solution_ContainersExample_DifferentContainersCombinationsWhenMinNumberOfContainersIsUsedEquals()
        {
            string containers = "20"
                + $"{Environment.NewLine}15"
                + $"{Environment.NewLine}10"
                + $"{Environment.NewLine}5"
                + $"{Environment.NewLine}5";

            Assert.Equal(3, task.Solution(containers));
        }
    }
}
