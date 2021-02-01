using System;
using App.Tasks.Year2017.Day2;
using Xunit;

namespace Tests.Tasks.Year2017.Day2
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_SpreadsheetNumbersExample_EvenlyDivisibleQuotientSpreadsheetChecksumEquals()
        {
            string spreadsheetNumbers = "5 9 2 8"
                + $"{Environment.NewLine}9 4 7 3"
                + $"{Environment.NewLine}3 8 6 5";

            Assert.Equal(9, task.Solution(spreadsheetNumbers));
        }
    }
}
