using System;
using App.Tasks.Year2020.Day14;
using Xunit;

namespace Tests.Tasks.Year2020.Day14
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_InitializationProgramExample_MemoryValuesSumForDecoderChipVersion2Equals()
        {
            string initializationProgram = "mask = 000000000000000000000000000000X1001X"
                + $"{Environment.NewLine}mem[42] = 100"
                + $"{Environment.NewLine}mask = 00000000000000000000000000000000X0XX"
                + $"{Environment.NewLine}mem[26] = 1";

            Assert.Equal(208, task.Solution(initializationProgram));
        }
    }
}
