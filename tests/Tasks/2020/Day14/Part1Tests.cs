using System;
using App.Tasks.Year2020.Day14;
using Xunit;

namespace Tests.Tasks.Year2020.Day14
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_InitializationProgramExample_MemoryValuesSumForDecoderChipVersion1Equals()
        {
            string initializationProgram = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X"
                + $"{Environment.NewLine}mem[8] = 11"
                + $"{Environment.NewLine}mem[7] = 101"
                + $"{Environment.NewLine}mem[8] = 0";

            Assert.Equal(165, task.Solution(initializationProgram));
        }
    }
}
