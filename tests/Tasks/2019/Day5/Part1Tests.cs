using App.Tasks.Year2019.Day5;
using Xunit;

namespace Tests.Tasks.Year2019.Day5
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("3,0,4,0,99", 1)]
        [InlineData("1002,4,3,4,33", 0)]
        [InlineData("1101,100,-1,4,0", 0)]
        public void Solution_IntegersExample_DiagnosticCodeEquals(string integers, int diagnosticCode)
        {
            Assert.Equal(diagnosticCode, task.Solution(integers));
        }
    }
}
