using System;
using App.Tasks.Year2018.Day2;
using Xunit;

namespace Tests.Tasks.Year2018.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_BoxIdsExample_BoxIdsChecksumEquals()
        {
            string boxIds = "abcdef"
                + $"{Environment.NewLine}bababc"
                + $"{Environment.NewLine}abbcde"
                + $"{Environment.NewLine}abcccd"
                + $"{Environment.NewLine}aabcdd"
                + $"{Environment.NewLine}abcdee"
                + $"{Environment.NewLine}ababab";

            Assert.Equal(12, task.Solution(boxIds));
        }
    }
}
