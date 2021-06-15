using App.Tasks.Year2019.Day7;
using Xunit;

namespace Tests.Tasks.Year2019.Day7
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", 43210)]
        [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,"
            + "101,5,23,23,1,24,23,23,4,23,99,0,0", 54321)]
        [InlineData("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,"
            + "1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", 65210)]
        public void Solution_IntegersExample_HighestSignalThatCanBeSentToTheThrustersEquals(
            string integers,
            int highestSignalThatCanBeSentToTheThrusters
        )
        {
            Assert.Equal(highestSignalThatCanBeSentToTheThrusters, task.Solution(integers));
        }
    }
}
