using System;
using App.Tasks.Year2018.Day7;
using Xunit;

namespace Tests.Tasks.Year2018.Day7
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InstructionsExample_OrderOfStepsInWhichInstructionsAreCompletedEquals()
        {
            string instructions = "Step C must be finished before step A can begin."
                + $"{Environment.NewLine}Step C must be finished before step F can begin."
                + $"{Environment.NewLine}Step A must be finished before step B can begin."
                + $"{Environment.NewLine}Step A must be finished before step D can begin."
                + $"{Environment.NewLine}Step B must be finished before step E can begin."
                + $"{Environment.NewLine}Step D must be finished before step E can begin."
                + $"{Environment.NewLine}Step F must be finished before step E can begin.";

            Assert.Equal("CABDFE", task.Solution(instructions));
        }
    }
}
