using System;
using App.Tasks.Year2017.Day24;
using Xunit;

namespace Tests.Tasks.Year2017.Day24
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ComponentsExample_StrengthOfStrongestBridgeWhichLengthIsLongestEquals()
        {
            string components = "0/2"
                + $"{Environment.NewLine}2/2"
                + $"{Environment.NewLine}2/3"
                + $"{Environment.NewLine}3/4"
                + $"{Environment.NewLine}3/5"
                + $"{Environment.NewLine}0/1"
                + $"{Environment.NewLine}10/1"
                + $"{Environment.NewLine}9/10";

            Assert.Equal(19, task.Solution(components));
        }
    }
}
