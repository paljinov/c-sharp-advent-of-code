using System;
using App.Tasks.Year2020.Day1;
using Xunit;

namespace Tests.Tasks.Year2020.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
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

            Assert.Equal(241861950, task.Solution(expenseReport));
        }
    }
}
