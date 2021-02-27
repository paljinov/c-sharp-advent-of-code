using System;
using System.Reflection;
using App.Tasks.Year2016.Day21;
using Xunit;

namespace Tests.Tasks.Year2016.Day21
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
            typeof(Part2)
                .GetField("scrambledPassword", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, "decab");
        }

        [Fact]
        public void Solution_OperationsExample_UnscrambledPasswordEquals()
        {
            string operations = $"{Environment.NewLine}swap position 4 with position 0"
                + $"{Environment.NewLine}swap letter d with letter b"
                + $"{Environment.NewLine}reverse positions 0 through 4"
                + $"{Environment.NewLine}rotate left 1 step"
                + $"{Environment.NewLine}move position 1 to position 4"
                + $"{Environment.NewLine}move position 3 to position 0"
                + $"{Environment.NewLine}rotate based on position of letter b"
                + $"{Environment.NewLine}rotate based on position of letter d";

            Assert.Equal("abcde", task.Solution(operations));
        }
    }
}
