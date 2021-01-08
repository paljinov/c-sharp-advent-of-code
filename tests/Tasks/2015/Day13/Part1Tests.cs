using System;
using App.Tasks.Year2015.Day13;
using Xunit;

namespace Tests.Tasks.Year2015.Day13
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_AttendeesSittingHappinessExample_TotalChangeInHappinessForOptimalSeatingArrangementEquals()
        {
            string attendeesSittingHappiness = "Alice would gain 54 happiness units by sitting next to Bob."
                + $"{Environment.NewLine}Alice would lose 79 happiness units by sitting next to Carol."
                + $"{Environment.NewLine}Alice would lose 2 happiness units by sitting next to David."
                + $"{Environment.NewLine}Bob would gain 83 happiness units by sitting next to Alice."
                + $"{Environment.NewLine}Bob would lose 7 happiness units by sitting next to Carol."
                + $"{Environment.NewLine}Bob would lose 63 happiness units by sitting next to David."
                + $"{Environment.NewLine}Carol would lose 62 happiness units by sitting next to Alice."
                + $"{Environment.NewLine}Carol would gain 60 happiness units by sitting next to Bob."
                + $"{Environment.NewLine}Carol would gain 55 happiness units by sitting next to David."
                + $"{Environment.NewLine}David would gain 46 happiness units by sitting next to Alice."
                + $"{Environment.NewLine}David would lose 7 happiness units by sitting next to Bob."
                + $"{Environment.NewLine}David would gain 41 happiness units by sitting next to Carol.";

            Assert.Equal(330, task.Solution(attendeesSittingHappiness));
        }
    }
}
