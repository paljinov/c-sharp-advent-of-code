using System;
using App.Tasks.Year2020.Day2;
using Xunit;

namespace Tests.Tasks.Year2020.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ExamplePasswordPolicies_ValidPasswordsEquals()
        {
            string passwordPolicies = "1-3 a: abcde"
                + $"{Environment.NewLine}1-3 b: cdefg"
                + $"{Environment.NewLine}2-9 c: ccccccccc";

            Assert.Equal(2, task.Solution(passwordPolicies));
        }
    }
}
