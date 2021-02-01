using System;
using App.Tasks.Year2017.Day2;
using Xunit;

namespace Tests.Tasks.Year2017.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_SpreadsheetNumbersExample_MaxMinSpreadsheetChecksumEquals()
        {
            string spreadsheetNumbers = "5 1 9 5"
                + $"{Environment.NewLine}7 5 3"
                + $"{Environment.NewLine}2 4 6 8";

            Assert.Equal(18, task.Solution(spreadsheetNumbers));
        }
    }
}
