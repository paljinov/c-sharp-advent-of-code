using System;
using App.Tasks.Year2017.Day13;
using Xunit;

namespace Tests.Tasks.Year2017.Day13
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirewallLayersExample_FewestNumberOfDelayedPicosecondsToAvoidBeingCaughtEquals()
        {
            string firewallLayers = $"{Environment.NewLine}0: 3"
                + $"{Environment.NewLine}1: 2"
                + $"{Environment.NewLine}4: 4"
                + $"{Environment.NewLine}6: 4";

            Assert.Equal(10, task.Solution(firewallLayers));
        }
    }
}
