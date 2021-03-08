using System;
using App.Tasks.Year2017.Day7;
using Xunit;

namespace Tests.Tasks.Year2017.Day7
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ProgramsListExample_BottomProgramNameEquals()
        {
            string programsList = $"{Environment.NewLine}pbga (66)"
                + $"{Environment.NewLine}xhth (57)"
                + $"{Environment.NewLine}ebii (61)"
                + $"{Environment.NewLine}havc (66)"
                + $"{Environment.NewLine}ktlj (57)"
                + $"{Environment.NewLine}fwft (72) -> ktlj, cntj, xhth"
                + $"{Environment.NewLine}qoyq (66)"
                + $"{Environment.NewLine}padx (45) -> pbga, havc, qoyq"
                + $"{Environment.NewLine}tknk (41) -> ugml, padx, fwft"
                + $"{Environment.NewLine}jptl (61)"
                + $"{Environment.NewLine}ugml (68) -> gyxo, ebii, jptl"
                + $"{Environment.NewLine}gyxo (61)"
                + $"{Environment.NewLine}cntj (57)";

            Assert.Equal("tknk", task.Solution(programsList));
        }
    }
}
