using System;
using App.Tasks.Year2022.Day5;
using Xunit;

namespace Tests.Tasks.Year2022.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_CratesStacksAndRearrangementProcedureExample_CratesWhichEndUpOnTopOfEachStackEquals()
        {
            string cratesStacksAndRearrangementProcedure = "    [D]    "
                + $"{Environment.NewLine}[N] [C]    "
                + $"{Environment.NewLine}[Z] [M] [P]"
                + $"{Environment.NewLine} 1   2   3 "
                + Environment.NewLine
                + $"{Environment.NewLine}move 1 from 2 to 1"
                + $"{Environment.NewLine}move 3 from 1 to 3"
                + $"{Environment.NewLine}move 2 from 2 to 1"
                + $"{Environment.NewLine}move 1 from 1 to 2";

            Assert.Equal("CMZ", task.Solution(cratesStacksAndRearrangementProcedure));
        }
    }
}
