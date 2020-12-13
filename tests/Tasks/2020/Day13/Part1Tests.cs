using System;
using App.Tasks.Year2020.Day13;
using Xunit;

namespace Tests.Tasks.Year2020.Day13
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_NotesExample_ProductOfEarliestBusIdAndMinutesWaitingEquals()
        {
            string notes = "939"
                + $"{Environment.NewLine}7,13,x,x,59,x,31,19";

            Assert.Equal(295, task.Solution(notes));
        }
    }
}
