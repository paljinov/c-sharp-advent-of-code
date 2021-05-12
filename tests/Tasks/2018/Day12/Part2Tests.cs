using System;
using App.Tasks.Year2018.Day12;
using Xunit;

namespace Tests.Tasks.Year2018.Day12
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_PotsAndSpreadNotesExample_NumbersSumOfAllPotsWhichContainPlantAfterGenerationsEquals()
        {
            string potsAndSpreadNotes = "initial state: #..#.#..##......###...###"
                + Environment.NewLine
                + $"{Environment.NewLine}...## => #"
                + $"{Environment.NewLine}..#.. => #"
                + $"{Environment.NewLine}.#... => #"
                + $"{Environment.NewLine}.#.#. => #"
                + $"{Environment.NewLine}.#.## => #"
                + $"{Environment.NewLine}.##.. => #"
                + $"{Environment.NewLine}.#### => #"
                + $"{Environment.NewLine}#.#.# => #"
                + $"{Environment.NewLine}#.### => #"
                + $"{Environment.NewLine}##.#. => #"
                + $"{Environment.NewLine}##.## => #"
                + $"{Environment.NewLine}###.. => #"
                + $"{Environment.NewLine}###.# => #"
                + $"{Environment.NewLine}####. => #";

            Assert.Equal(1099999999210, task.Solution(potsAndSpreadNotes));
        }
    }
}
