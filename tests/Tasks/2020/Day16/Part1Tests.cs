using System;
using App.Tasks.Year2020.Day16;
using Xunit;

namespace Tests.Tasks.Year2020.Day16
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_NotesExample_TicketScanningErrorRateEquals()
        {
            string notes = "class: 1-3 or 5-7"
                + $"{Environment.NewLine}row: 6-11 or 33-44"
                + $"{Environment.NewLine}seat: 13-40 or 45-50"
                + Environment.NewLine
                + $"{Environment.NewLine}your ticket:"
                + $"{Environment.NewLine}7,1,14"
                + Environment.NewLine
                + $"{Environment.NewLine}nearby tickets:"
                + $"{Environment.NewLine}7,3,47"
                + $"{Environment.NewLine}40,4,50"
                + $"{Environment.NewLine}55,2,20"
                + $"{Environment.NewLine}38,6,12";

            Assert.Equal(71, task.Solution(notes));
        }
    }
}
