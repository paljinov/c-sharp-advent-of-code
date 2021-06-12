using App.Tasks.Year2019.Day5;
using Xunit;

namespace Tests.Tasks.Year2019.Day5
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 1)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 1)]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 1)]
        public void Solution_IntegersExample_DiagnosticCodeEquals(string integers, int diagnosticCode)
        {
            Assert.Equal(diagnosticCode, task.Solution(integers));
        }
    }
}
