using System.Reflection;
using App.Tasks.Year2017.Day10;
using Xunit;

namespace Tests.Tasks.Year2017.Day10
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            Process process = new Process();
            typeof(Process)
                .GetField("circularListLength", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(process, 5);
            typeof(Part1)
                .GetField("process", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, process);
        }

        [Fact]
        public void Solution_LengthsSequenceExample_FirstTwoCircularListNumbersProductEquals()
        {
            string lengthsSequence = "3, 4, 1, 5";
            Assert.Equal(12, task.Solution(lengthsSequence));
        }
    }
}
