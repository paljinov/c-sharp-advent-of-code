using System;
using App.Tasks.Year2018.Day13;
using Xunit;

namespace Tests.Tasks.Year2018.Day13
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_TracksMapExample_LocationOfTheLastCartThatHasNotCrashedEquals()
        {
            string tracksMap = "/>-<\\  "
                + $"{Environment.NewLine}|   |  "
                + $"{Environment.NewLine}| /<+-\\"
                + $"{Environment.NewLine}| | | v"
                + $"{Environment.NewLine}\\>+</ |"
                + $"{Environment.NewLine}  |   ^"
                + $"{Environment.NewLine}  \\<->/";

            Assert.Equal("6,4", task.Solution(tracksMap));
        }
    }
}
