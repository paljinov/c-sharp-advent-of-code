using System;
using System.Reflection;
using App.Tasks.Year2016.Day20;
using Xunit;

namespace Tests.Tasks.Year2016.Day20
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            IpAddresses ipAddresses = new IpAddresses();
            typeof(IpAddresses)
                .GetField("ipAddressesRange", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(ipAddresses, ((uint)0, (uint)9));
            typeof(Part1)
                .GetField("ipAddresses", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, ipAddresses);
        }

        [Fact]
        public void Solution_BlacklistRangesExample_LowestValuedIpAddressThatIsNotBlockedEquals()
        {
            string blacklistRanges = "5-8"
                + $"{Environment.NewLine}0-2"
                + $"{Environment.NewLine}4-7";

            Assert.Equal((uint)3, task.Solution(blacklistRanges));
        }
    }
}
