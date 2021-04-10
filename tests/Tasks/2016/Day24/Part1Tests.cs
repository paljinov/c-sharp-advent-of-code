using System;
using App.Tasks.Year2016.Day24;
using Xunit;

namespace Tests.Tasks.Year2016.Day24
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_MapExample_RegisterAValueEquals()
        {
            string map = "###########"
                + $"{Environment.NewLine}#0.1.....2#"
                + $"{Environment.NewLine}#.#######.#"
                + $"{Environment.NewLine}#4.......3#"
                + $"{Environment.NewLine}###########";

            Assert.Equal(14, task.Solution(map));
        }
    }
}
