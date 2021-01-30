using System;
using App.Tasks.Year2016.Day7;
using Xunit;

namespace Tests.Tasks.Year2016.Day7
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("aba[bab]xyz")]
        [InlineData("aaa[kek]eke")]
        [InlineData("zazbz[bzb]cdb")]
        public void Solution_IpAddressExample_IpAddressSupportsSslTrue(string ipAddress)
        {
            Assert.True(Convert.ToBoolean(task.Solution(ipAddress)));
        }

        [Theory]
        [InlineData("xyx[xyx]xyx")]
        public void Solution_IpAddressExample_IpAddressSupportsSslFalse(string ipAddress)
        {
            Assert.False(Convert.ToBoolean(task.Solution(ipAddress)));
        }
    }
}
