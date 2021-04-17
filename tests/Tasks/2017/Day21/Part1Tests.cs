using System;
using System.Reflection;
using App.Tasks.Year2017.Day21;
using Xunit;

namespace Tests.Tasks.Year2017.Day21
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
            typeof(Part1)
                .GetField("totalIterations", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 2);
        }

        [Fact]
        public void Solution_RulesExample_TurnedOnPixelsAfterIterationsCountEquals()
        {
            string rules = "../.# => ##./#../..."
               + $"{Environment.NewLine}.#./..#/### => #..#/..../..../#..#";

            Assert.Equal(12, task.Solution(rules));
        }
    }
}
