using System;
using App.Tasks.Year2022.Day5;
using Xunit;

namespace Tests.Tasks.Year2022.Day5
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_CratesStacksAndRearrangementProcedureExample_CratesWhichEndUpOnTopOfEachStackWhenMovingMultipleCratesAtOnceEquals()
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

            Assert.Equal("MCD", task.Solution(cratesStacksAndRearrangementProcedure));
        }
    }
}
