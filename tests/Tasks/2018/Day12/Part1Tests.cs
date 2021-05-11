using System;
using App.Tasks.Year2018.Day12;
using Xunit;

namespace Tests.Tasks.Year2018.Day12
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
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

            Assert.Equal(325, task.Solution(potsAndSpreadNotes));
        }
    }
}
