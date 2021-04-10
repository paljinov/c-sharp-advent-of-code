using System;
using App.Tasks.Year2016.Day24;
using Xunit;

namespace Tests.Tasks.Year2016.Day24
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_MapExample_RegisterAValueEquals()
        {
            string map = "###########"
                + $"{Environment.NewLine}#0.1.....2#"
                + $"{Environment.NewLine}#.#######.#"
                + $"{Environment.NewLine}#4.......3#"
                + $"{Environment.NewLine}###########";

            Assert.Equal(20, task.Solution(map));
        }
    }
}
