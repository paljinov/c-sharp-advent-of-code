using System;
using App.Tasks.Year2020.Day25;
using Xunit;

namespace Tests.Tasks.Year2020.Day25
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_PublicKeysExample_HandshakeEncryptionKeyEquals()
        {
            string tiles = "5764801"
                + $"{Environment.NewLine}17807724";

            Assert.Equal(14897079, task.Solution(tiles));
        }
    }
}
