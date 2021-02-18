using System.Reflection;
using App.Tasks.Year2016.Day16;
using Xunit;

namespace Tests.Tasks.Year2016.Day16
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            task.GetType()
                .GetField("diskLength", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 20);
        }

        [Fact]
        public void Solution_InitialStateExample_ChecksumEquals()
        {
            string initialState = "10000";
            Assert.Equal("01100", task.Solution(initialState));
        }
    }
}
