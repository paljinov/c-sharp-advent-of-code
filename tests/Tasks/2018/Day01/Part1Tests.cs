using System;
using App.Tasks.Year2018.Day1;
using Xunit;

namespace Tests.Tasks.Year2018.Day1
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("+1\n-2\n+3\n+1", 3)]
        [InlineData("+1\n+1\n+1", 3)]
        [InlineData("+1\n+1\n-2", 0)]
        [InlineData("-1\n-2\n-3", -6)]
        public void Solution_FrequencyChangesExample_ResultingFrequencyEquals(
            string frequencyChanges,
            int resultingFrequency
        )
        {
            Assert.Equal(resultingFrequency, task.Solution(frequencyChanges));
        }
    }
}
