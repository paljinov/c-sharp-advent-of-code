using System.Reflection;
using App.Tasks.Year2016.Day18;
using Xunit;

namespace Tests.Tasks.Year2016.Day18
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
            task.GetType()
                .GetField("totalRows", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 10);
        }

        [Theory]
        [InlineData("..^^.", 16)]
        [InlineData(".^^.^.^^^^", 38)]
        public void Solution_InitialTilesRowExample_SafeTilesTotalEquals(string initialTilesRow, int safeTiles)
        {
            Assert.Equal(safeTiles, task.Solution(initialTilesRow));
        }
    }
}
