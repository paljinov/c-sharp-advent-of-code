using System;
using System.Reflection;
using App.Tasks.Year2020.Day16;
using Xunit;

namespace Tests.Tasks.Year2020.Day16
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            Tickets tickets = new Tickets();
            typeof(Tickets)
                .GetField("fieldStartsWith", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(tickets, "class");

            typeof(Part2)
                .GetField("tickets", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, tickets);
        }

        [Fact]
        public void Solution_NotesExample_TicketDepartureFieldsProductEquals()
        {
            string notes = "class: 0-1 or 4-19"
                + $"{Environment.NewLine}row: 0-5 or 8-19"
                + $"{Environment.NewLine}seat: 0-13 or 16-19"
                + Environment.NewLine
                + $"{Environment.NewLine}your ticket:"
                + $"{Environment.NewLine}11,12,13"
                + Environment.NewLine
                + $"{Environment.NewLine}nearby tickets:"
                + $"{Environment.NewLine}3,9,18"
                + $"{Environment.NewLine}15,1,5"
                + $"{Environment.NewLine}5,14,9";

            Assert.Equal(12, task.Solution(notes));
        }
    }
}
