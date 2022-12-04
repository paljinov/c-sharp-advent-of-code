using System;
using App.Tasks.Year2022.Day4;
using Xunit;

namespace Tests.Tasks.Year2022.Day4
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_SectionAssignmentPairsExample_SectionAssignmentPairsWhereRangesOverlapCountEquals()
        {
            string sectionAssignmentPairs = "2-4,6-8"
                + $"{Environment.NewLine}2-3,4-5"
                + $"{Environment.NewLine}5-7,7-9"
                + $"{Environment.NewLine}2-8,3-7"
                + $"{Environment.NewLine}6-6,4-6"
                + $"{Environment.NewLine}2-6,4-8";

            Assert.Equal(4, task.Solution(sectionAssignmentPairs));
        }
    }
}
