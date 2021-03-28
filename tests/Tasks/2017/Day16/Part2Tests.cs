using System.Reflection;
using App.Tasks.Year2017.Day16;
using Xunit;

namespace Tests.Tasks.Year2017.Day16
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            Dance dance = new Dance();
            typeof(Dance)
                .GetField("totalPrograms", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(dance, 5);
            typeof(Part2)
                .GetField("dance", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, dance);
        }

        [Fact]
        public void Solution_DanceMovesExample_ProgramsOrderAfterBillionDancesEquals()
        {
            string danceMoves = "s1,x3/4,pe/b";
            Assert.Equal("abcde", task.Solution(danceMoves));
        }
    }
}
