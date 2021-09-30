using System;
using App.Tasks.Year2019.Day24;
using Xunit;

namespace Tests.Tasks.Year2019.Day24
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_AreaGridExample_BiodiversityRatingForTheFirstLayoutThatAppearsTwiceEquals()
        {
            string areaGrid = "....#"
                + $"{Environment.NewLine}#..#."
                + $"{Environment.NewLine}#..##"
                + $"{Environment.NewLine}..#.."
                + $"{Environment.NewLine}#....";

            Assert.Equal(2129920, task.Solution(areaGrid));
        }
    }
}
