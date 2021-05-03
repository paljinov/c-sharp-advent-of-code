using System;
using System.Reflection;
using App.Tasks.Year2018.Day7;
using Xunit;

namespace Tests.Tasks.Year2018.Day7
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
            task.GetType()
                .GetField("totalWorkers", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 2);
            task.GetType()
                .GetField("baseStepDuration", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 0);
        }

        [Fact]
        public void Solution_InstructionsExample_SecondsNeededToCompleteAllStepsEquals()
        {
            string instructions = "Step C must be finished before step A can begin."
                + $"{Environment.NewLine}Step C must be finished before step F can begin."
                + $"{Environment.NewLine}Step A must be finished before step B can begin."
                + $"{Environment.NewLine}Step A must be finished before step D can begin."
                + $"{Environment.NewLine}Step B must be finished before step E can begin."
                + $"{Environment.NewLine}Step D must be finished before step E can begin."
                + $"{Environment.NewLine}Step F must be finished before step E can begin.";

            Assert.Equal(15, task.Solution(instructions));
        }
    }
}
