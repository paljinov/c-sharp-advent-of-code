using System;
using System.Collections;
using System.Collections.Generic;
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
        [ClassData(typeof(FrequencyChanges_ResultingFrequencyAfterAllChanges_TestData))]
        public void Solution_FrequencyChangesExample_ResultingFrequencyEquals(
            string frequencyChanges,
            int resultingFrequencyAfterAllChanges
        )
        {
            Assert.Equal(resultingFrequencyAfterAllChanges, task.Solution(frequencyChanges));
        }
    }

    public class FrequencyChanges_ResultingFrequencyAfterAllChanges_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { $"+1{Environment.NewLine}-2{Environment.NewLine}+3{Environment.NewLine}+1", 3 };
            yield return new object[] { $"+1{Environment.NewLine}+1{Environment.NewLine}+1", 3 };
            yield return new object[] { $"+1{Environment.NewLine}+1{Environment.NewLine}-2", 0 };
            yield return new object[] { $"-1{Environment.NewLine}-2{Environment.NewLine}-3", -6 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
