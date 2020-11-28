using System;
using App.Tasks.Year2015.Day7;
using Xunit;

namespace Tests.Tasks.Year2015.Day7
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ExampleCircuit_WireASignalEquals()
        {
            string circuit = $"{Environment.NewLine}123 -> x"
                + $"{Environment.NewLine}456 -> y"
                + $"{Environment.NewLine}x AND y -> d"
                + $"{Environment.NewLine}x OR y -> e"
                + $"{Environment.NewLine}x LSHIFT 2 -> f"
                + $"{Environment.NewLine}y RSHIFT 2 -> g"
                + $"{Environment.NewLine}NOT x -> h"
                + $"{Environment.NewLine}NOT y -> i"
                + $"{Environment.NewLine}y -> a";

            Assert.Equal(456, task.Solution(circuit));
        }
    }
}
