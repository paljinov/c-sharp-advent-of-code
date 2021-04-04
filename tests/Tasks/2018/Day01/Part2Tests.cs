using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2018.Day1;
using Xunit;

namespace Tests.Tasks.Year2018.Day1
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [ClassData(typeof(FrequencyChanges_FirstFrequencyDeviceReachesTwice_TestData))]
        public void Solution_FrequencyChangesExample_FirstFrequencyDeviceReachesTwiceEquals(
            string frequencyChanges,
            int firstFrequencyDeviceReachesTwice
        )
        {
            Assert.Equal(firstFrequencyDeviceReachesTwice, task.Solution(frequencyChanges));
        }
    }

    public class FrequencyChanges_FirstFrequencyDeviceReachesTwice_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { $"+1{Environment.NewLine}-2{Environment.NewLine}+3{Environment.NewLine}+1", 2 };
            yield return new object[] { $"+1{Environment.NewLine}-1", 0 };
            yield return new object[]
            {
                $"+3{Environment.NewLine}+3{Environment.NewLine}+4{Environment.NewLine}-2{Environment.NewLine}-4",
                10
            };
            yield return new object[]
            {
                $"-6{Environment.NewLine}+3{Environment.NewLine}+8{Environment.NewLine}+5{Environment.NewLine}-6",
                5
            };
            yield return new object[]
            {
                $"+7{Environment.NewLine}+7{Environment.NewLine}-2{Environment.NewLine}-7{Environment.NewLine}-4",
                14
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
