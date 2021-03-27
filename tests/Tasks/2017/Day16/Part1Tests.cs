using System.Reflection;
using App.Tasks.Year2017.Day16;
using Xunit;

namespace Tests.Tasks.Year2017.Day16
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            Dance dance = new Dance();
            typeof(Dance)
                .GetField("totalPrograms", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(dance, 5);
            typeof(Part1)
                .GetField("dance", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, dance);
        }

        [Fact]
        public void Solution_DanceMovesExample_ProgramsOrderAfterDanceEquals()
        {
            string danceMoves = "s1,x3/4,pe/b";
            Assert.Equal("baedc", task.Solution(danceMoves));
        }
    }
}
