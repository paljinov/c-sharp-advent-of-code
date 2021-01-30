using System;
using App.Tasks.Year2016.Day7;
using Xunit;

namespace Tests.Tasks.Year2016.Day7
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("abba[mnop]qrst")]
        [InlineData("ioxxoj[asdfgh]zxcvbn")]
        public void Solution_IpAddressExample_IpAddressSupportsTlsTrue(string ipAddress)
        {
            Assert.True(Convert.ToBoolean(task.Solution(ipAddress)));
        }

        [Theory]
        [InlineData("abcd[bddb]xyyx")]
        [InlineData("aaaa[qwer]tyui")]
        public void Solution_IpAddressExample_IpAddressSupportsTlsFalse(string ipAddress)
        {
            Assert.False(Convert.ToBoolean(task.Solution(ipAddress)));
        }
    }
}
