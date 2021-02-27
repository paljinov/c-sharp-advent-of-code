using System;
using System.Reflection;
using App.Tasks.Year2016.Day21;
using Xunit;

namespace Tests.Tasks.Year2016.Day21
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
            typeof(Part1)
                .GetField("unscrambledPassword", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, "abcde");
        }

        [Fact]
        public void Solution_OperationsExample_ScrambledPasswordEquals()
        {
            string operations = $"{Environment.NewLine}swap position 4 with position 0"
                + $"{Environment.NewLine}swap letter d with letter b"
                + $"{Environment.NewLine}reverse positions 0 through 4"
                + $"{Environment.NewLine}rotate left 1 step"
                + $"{Environment.NewLine}move position 1 to position 4"
                + $"{Environment.NewLine}move position 3 to position 0"
                + $"{Environment.NewLine}rotate based on position of letter b"
                + $"{Environment.NewLine}rotate based on position of letter d";

            Assert.Equal("decab", task.Solution(operations));
        }
    }
}
