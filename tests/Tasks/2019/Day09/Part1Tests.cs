using App.Tasks.Year2019.Day9;
using Xunit;

namespace Tests.Tasks.Year2019.Day9
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", 99)]
        [InlineData("1102,34915192,34915192,7,4,7,99,0", 1219070632396864)]
        [InlineData("104,1125899906842624,99", 1125899906842624)]
        public void Solution_IntegersExample_BoostKeycodeEquals(string integers, long boostKeycode)
        {
            Assert.Equal(boostKeycode, task.Solution(integers));
        }
    }
}
