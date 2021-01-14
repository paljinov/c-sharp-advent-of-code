using System;
using System.Reflection;
using App.Tasks.Year2015.Day17;
using Xunit;

namespace Tests.Tasks.Year2015.Day17
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            ContainersCombinations containersCombinations = new ContainersCombinations();
            containersCombinations.GetType()
                .GetField("eggnogLiters", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(containersCombinations, 25);

            task.GetType()
                .GetField("containersCombinations", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, containersCombinations);
        }

        [Fact]
        public void Solution_ContainersExample_DifferentContainersCombinationsEquals()
        {
            string containers = "20"
                + $"{Environment.NewLine}15"
                + $"{Environment.NewLine}10"
                + $"{Environment.NewLine}5"
                + $"{Environment.NewLine}5";

            Assert.Equal(4, task.Solution(containers));
        }
    }
}
