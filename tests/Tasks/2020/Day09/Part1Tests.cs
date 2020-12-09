using System;
using System.Reflection;
using App.Tasks.Year2020.Day9;
using Xunit;

namespace Tests.Tasks.Year2020.Day9
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            NumbersFinder numbersFinder = new NumbersFinder();
            // Preamble of 5 numbers
            typeof(NumbersFinder)
                .GetField("preamble", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(numbersFinder, 5);

            typeof(Part1)
                .GetField("numbersFinder", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, numbersFinder);
        }

        [Fact]
        public void Solution_NumberListExample_FirstNumberWhichIsNotSumOfTwoPreambleNumbersEquals()
        {
            string numberList = "35"
                + $"{Environment.NewLine}20"
                + $"{Environment.NewLine}15"
                + $"{Environment.NewLine}25"
                + $"{Environment.NewLine}47"
                + $"{Environment.NewLine}40"
                + $"{Environment.NewLine}62"
                + $"{Environment.NewLine}55"
                + $"{Environment.NewLine}65"
                + $"{Environment.NewLine}95"
                + $"{Environment.NewLine}102"
                + $"{Environment.NewLine}117"
                + $"{Environment.NewLine}150"
                + $"{Environment.NewLine}182"
                + $"{Environment.NewLine}127"
                + $"{Environment.NewLine}219"
                + $"{Environment.NewLine}299"
                + $"{Environment.NewLine}277"
                + $"{Environment.NewLine}309"
                + $"{Environment.NewLine}576";

            Assert.Equal(127, task.Solution(numberList));
        }
    }
}
