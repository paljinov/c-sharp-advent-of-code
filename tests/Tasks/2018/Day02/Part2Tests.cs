using System;
using App.Tasks.Year2018.Day2;
using Xunit;

namespace Tests.Tasks.Year2018.Day2
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_BoxIdsExample_CommonLettersBetweenTwoCorrectBoxIdsEquals()
        {
            string boxIds = "abcde"
                + $"{Environment.NewLine}fghij"
                + $"{Environment.NewLine}klmno"
                + $"{Environment.NewLine}pqrst"
                + $"{Environment.NewLine}fguij"
                + $"{Environment.NewLine}axcye"
                + $"{Environment.NewLine}wvxyz";

            Assert.Equal("fgij", task.Solution(boxIds));
        }
    }
}
