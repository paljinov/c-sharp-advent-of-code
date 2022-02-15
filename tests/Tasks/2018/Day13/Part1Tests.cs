using System;
using App.Tasks.Year2018.Day13;
using Xunit;

namespace Tests.Tasks.Year2018.Day13
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_TracksMapExample_FirstCrashLocationEquals()
        {
            string tracksMap = "/->-\\        "
                + $"{Environment.NewLine}|   |  /----\\"
                + $"{Environment.NewLine}| /-+--+-\\  |"
                + $"{Environment.NewLine}| | |  | v  |"
                + $"{Environment.NewLine}\\-+-/  \\-+--/"
                + $"{Environment.NewLine}  \\------/   ";

            Assert.Equal("7,3", task.Solution(tracksMap));
        }
    }
}
