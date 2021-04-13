using System;
using App.Tasks.Year2017.Day19;
using Xunit;

namespace Tests.Tasks.Year2017.Day19
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_RoutingDiagramExample_LettersSeenByLittlePacketEquals()
        {
            string routingDiagram = "     |          "
                + $"{Environment.NewLine}     |  +--+    "
                + $"{Environment.NewLine}     A  |  C    "
                + $"{Environment.NewLine} F---|----E|--+ "
                + $"{Environment.NewLine}     |  |  |  D "
                + $"{Environment.NewLine}     +B-+  +--+ ";

            Assert.Equal("ABCDEF", task.Solution(routingDiagram));
        }
    }
}
