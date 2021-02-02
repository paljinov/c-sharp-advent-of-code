using System;
using App.Tasks.Year2020.Day13;
using Xunit;

namespace Tests.Tasks.Year2020.Day13
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("7,13,x,x,59,x,31,19", 1068781)]
        [InlineData("17,x,13,19", 3417)]
        [InlineData("67,7,59,61", 754018)]
        [InlineData("67,x,7,59,61", 779210)]
        [InlineData("67,7,x,59,61", 1261476)]
        [InlineData("1789,37,47,1889", 1202161486)]
        public void Solution_NotesExample_EarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositionsEquals(
            string notes,
            int earliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositions
        )
        {
            notes = $"0{Environment.NewLine}{notes}";
            Assert.Equal(earliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositions, task.Solution(notes));
        }
    }
}
