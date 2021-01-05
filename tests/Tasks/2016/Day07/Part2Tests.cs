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

        [Fact]
        public void Solution_FirstIpAddressExample_IpAddressSupportsSslTrue()
        {
            Assert.True(Convert.ToBoolean(task.Solution("aba[bab]xyz")));
        }

        [Fact]
        public void Solution_SecondIpAddressExample_IpAddressSupportsSslFalse()
        {
            Assert.False(Convert.ToBoolean(task.Solution("xyx[xyx]xyx")));
        }

        [Fact]
        public void Solution_ThirdIpAddressExample_IpAddressSupportsSslTrue()
        {
            Assert.True(Convert.ToBoolean(task.Solution("aaa[kek]eke")));
        }

        [Fact]
        public void Solution_FourthIpAddressExample_IpAddressSupportsSslTrue()
        {
            Assert.True(Convert.ToBoolean(task.Solution("zazbz[bzb]cdb")));
        }
    }
}
