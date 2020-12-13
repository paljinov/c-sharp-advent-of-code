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

        [Fact]
        public void Solution_FirstNotesExample_EarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositionsEquals()
        {
            string notes = "0"
                + $"{Environment.NewLine}7,13,x,x,59,x,31,19";

            Assert.Equal(1068781, task.Solution(notes));
        }

        [Fact]
        public void Solution_SecondNotesExample_EarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositionsEquals()
        {
            string notes = "0"
                + $"{Environment.NewLine}17,x,13,19";

            Assert.Equal(3417, task.Solution(notes));
        }

        [Fact]
        public void Solution_ThirdNotesExample_EarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositionsEquals()
        {
            string notes = "0"
                + $"{Environment.NewLine}67,7,59,61";

            Assert.Equal(754018, task.Solution(notes));
        }

        [Fact]
        public void Solution_FourthNotesExample_EarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositionsEquals()
        {
            string notes = "0"
                + $"{Environment.NewLine}67,x,7,59,61";

            Assert.Equal(779210, task.Solution(notes));
        }

        [Fact]
        public void Solution_FifthNotesExample_EarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositionsEquals()
        {
            string notes = "0"
                + $"{Environment.NewLine}67,7,x,59,61";

            Assert.Equal(1261476, task.Solution(notes));
        }

        [Fact]
        public void Solution_SixthNotesExample_EarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositionsEquals()
        {
            string notes = "0"
                + $"{Environment.NewLine}1789,37,47,1889";

            Assert.Equal(1202161486, task.Solution(notes));
        }
    }
}
