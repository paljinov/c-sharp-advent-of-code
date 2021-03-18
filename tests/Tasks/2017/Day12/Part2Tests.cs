using System;
using App.Tasks.Year2017.Day12;
using Xunit;

namespace Tests.Tasks.Year2017.Day12
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ProgramsListExample_TotalGroupsCountEquals()
        {
            string programList = $"{Environment.NewLine}0 <-> 2"
                + $"{Environment.NewLine}1 <-> 1"
                + $"{Environment.NewLine}2 <-> 0, 3, 4"
                + $"{Environment.NewLine}3 <-> 2, 4"
                + $"{Environment.NewLine}4 <-> 2, 3, 6"
                + $"{Environment.NewLine}5 <-> 6"
                + $"{Environment.NewLine}6 <-> 4, 5";

            Assert.Equal(2, task.Solution(programList));
        }
    }
}
