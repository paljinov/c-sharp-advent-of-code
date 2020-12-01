using System;
using App.Tasks.Year2020.Day1;
using Xunit;

namespace Tests.Tasks.Year2020.Day1
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ExampleExpenseReport_ProductEquals()
        {
            string expenseReport = "1721"
                + $"{Environment.NewLine}979"
                + $"{Environment.NewLine}366"
                + $"{Environment.NewLine}299"
                + $"{Environment.NewLine}675"
                + $"{Environment.NewLine}1456";

            Assert.Equal(514579, task.Solution(expenseReport));
        }
    }
}
