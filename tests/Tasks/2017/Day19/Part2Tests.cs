using System;
using App.Tasks.Year2017.Day19;
using Xunit;

namespace Tests.Tasks.Year2017.Day19
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_RoutingDiagramExample_StepsMadeByLittlePacketEquals()
        {
            string routingDiagram = "     |          "
                + $"{Environment.NewLine}     |  +--+    "
                + $"{Environment.NewLine}     A  |  C    "
                + $"{Environment.NewLine} F---|----E|--+ "
                + $"{Environment.NewLine}     |  |  |  D "
                + $"{Environment.NewLine}     +B-+  +--+ ";

            Assert.Equal(38, task.Solution(routingDiagram));
        }
    }
}
