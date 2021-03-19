using System;
using App.Tasks.Year2017.Day13;
using Xunit;

namespace Tests.Tasks.Year2017.Day13
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirewallLayersExample_TripSeverityEquals()
        {
            string firewallLayers = $"{Environment.NewLine}0: 3"
                + $"{Environment.NewLine}1: 2"
                + $"{Environment.NewLine}4: 4"
                + $"{Environment.NewLine}6: 4";

            Assert.Equal(24, task.Solution(firewallLayers));
        }
    }
}
