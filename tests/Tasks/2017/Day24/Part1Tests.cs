using System;
using App.Tasks.Year2017.Day24;
using Xunit;

namespace Tests.Tasks.Year2017.Day24
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ComponentsExample_StrongestBridgeStrengthEquals()
        {
            string components = "0/2"
                + $"{Environment.NewLine}2/2"
                + $"{Environment.NewLine}2/3"
                + $"{Environment.NewLine}3/4"
                + $"{Environment.NewLine}3/5"
                + $"{Environment.NewLine}0/1"
                + $"{Environment.NewLine}10/1"
                + $"{Environment.NewLine}9/10";

            Assert.Equal(31, task.Solution(components));
        }
    }
}
