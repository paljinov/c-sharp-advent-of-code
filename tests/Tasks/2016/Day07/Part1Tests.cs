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

        [Fact]
        public void Solution_FirstIpAddressExample_IpAddressSupportsTlsTrue()
        {
            Assert.True(Convert.ToBoolean(task.Solution("abba[mnop]qrst")));
        }

        [Fact]
        public void Solution_SecondIpAddressExample_IpAddressSupportsTlsFalse()
        {
            Assert.False(Convert.ToBoolean(task.Solution("abcd[bddb]xyyx")));
        }

        [Fact]
        public void Solution_ThirdIpAddressExample_IpAddressSupportsTlsFalse()
        {
            Assert.False(Convert.ToBoolean(task.Solution("aaaa[qwer]tyui")));
        }

        [Fact]
        public void Solution_FourthIpAddressExample_IpAddressSupportsTlsTrue()
        {
            Assert.True(Convert.ToBoolean(task.Solution("ioxxoj[asdfgh]zxcvbn")));
        }
    }
}
