using System;
using App.Tasks.Year2017.Day25;
using Xunit;

namespace Tests.Tasks.Year2017.Day25
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_BlueprintExample_DiagnosticChecksumEquals()
        {
            string blueprint = "Begin in state A."
                + $"{Environment.NewLine}Perform a diagnostic checksum after 6 steps."
                + Environment.NewLine
                + $"{Environment.NewLine}In state A:"
                + $"{Environment.NewLine}  If the current value is 0:"
                + $"{Environment.NewLine}    - Write the value 1."
                + $"{Environment.NewLine}    - Move one slot to the right."
                + $"{Environment.NewLine}    - Continue with state B."
                + $"{Environment.NewLine}  If the current value is 1:"
                + $"{Environment.NewLine}    - Write the value 0."
                + $"{Environment.NewLine}    - Move one slot to the left."
                + $"{Environment.NewLine}    - Continue with state B."
                + Environment.NewLine
                + $"{Environment.NewLine}In state B:"
                + $"{Environment.NewLine}  If the current value is 0:"
                + $"{Environment.NewLine}    - Write the value 1."
                + $"{Environment.NewLine}    - Move one slot to the left."
                + $"{Environment.NewLine}    - Continue with state A."
                + $"{Environment.NewLine}  If the current value is 1:"
                + $"{Environment.NewLine}    - Write the value 1."
                + $"{Environment.NewLine}    - Move one slot to the right."
                + $"{Environment.NewLine}    - Continue with state A.";

            Assert.Equal(3, task.Solution(blueprint));
        }
    }
}
