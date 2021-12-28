using System;
using App.Tasks.Year2021.Day25;
using Xunit;

namespace Tests.Tasks.Year2021.Day25
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_SeaCucumbersLocationsMapExample_FirstStepOnWhichNoSeaCucumbersMoveEquals()
        {
            string seaCucumbersLocationsMap = "v...>>.vv>"
                + $"{Environment.NewLine}.vv>>.vv.."
                + $"{Environment.NewLine}>>.>v>...v"
                + $"{Environment.NewLine}>>v>>.>.v."
                + $"{Environment.NewLine}v>v.vv.v.."
                + $"{Environment.NewLine}>.>>..v..."
                + $"{Environment.NewLine}.vv..>.>v."
                + $"{Environment.NewLine}v.v..>>v.v"
                + $"{Environment.NewLine}....v..v.>";

            Assert.Equal(58, task.Solution(seaCucumbersLocationsMap));
        }
    }
}
