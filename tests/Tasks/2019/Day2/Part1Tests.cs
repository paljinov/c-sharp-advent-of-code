using System.Reflection;
using App.Tasks.Year2019.Day2;
using Xunit;

namespace Tests.Tasks.Year2019.Day2
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
            task.GetType()
                .GetField("replacePositions", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, false);
        }

        [Theory]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", 3500)]
        [InlineData("1,0,0,0,99", 2)]
        [InlineData("2,3,0,3,99", 2)]
        [InlineData("2,4,4,5,99,0", 2)]
        [InlineData("1,1,1,4,99,5,6,0,99", 30)]
        public void Solution_IntegersExample_ValueLeftAtFirstPositionAfterProgramHaltsEquals(
            string integers,
            int valueLeftAtFirstPositionAfterProgramHalts
        )
        {
            Assert.Equal(valueLeftAtFirstPositionAfterProgramHalts, task.Solution(integers));
        }
    }
}
