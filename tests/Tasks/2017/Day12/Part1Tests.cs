using System;
using App.Tasks.Year2017.Day12;
using Xunit;

namespace Tests.Tasks.Year2017.Day12
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ProgramsListExample_ProgramsInTheGroupThatContainsProgramWithId0CountEquals()
        {
            string programList = $"{Environment.NewLine}0 <-> 2"
                + $"{Environment.NewLine}1 <-> 1"
                + $"{Environment.NewLine}2 <-> 0, 3, 4"
                + $"{Environment.NewLine}3 <-> 2, 4"
                + $"{Environment.NewLine}4 <-> 2, 3, 6"
                + $"{Environment.NewLine}5 <-> 6"
                + $"{Environment.NewLine}6 <-> 4, 5";

            Assert.Equal(6, task.Solution(programList));
        }
    }
}
