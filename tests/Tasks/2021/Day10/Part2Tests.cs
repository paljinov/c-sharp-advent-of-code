using System;
using App.Tasks.Year2021.Day10;
using Xunit;

namespace Tests.Tasks.Year2021.Day10
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_NavigationSubsystemExample_AutocompleteToolsMiddleScoreEquals()
        {
            string navigationSubsystem = "[({(<(())[]>[[{[]{<()<>>"
                + Environment.NewLine + "[(()[<>])]({[<{<<[]>>("
                + Environment.NewLine + "{([(<{}[<>[]}>{[]{[(<()>"
                + Environment.NewLine + "(((({<>}<{<{<>}{[]{[]{}"
                + Environment.NewLine + "[[<[([]))<([[{}[[()]]]"
                + Environment.NewLine + "[{[{({}]{}}([{[{{{}}([]"
                + Environment.NewLine + "{<[[]]>}<{[{[{[]{()[[[]"
                + Environment.NewLine + "[<(<(<(<{}))><([]([]()"
                + Environment.NewLine + "<{([([[(<>()){}]>(<<{{"
                + Environment.NewLine + "<{([{{}}[<[[[<>{}]]]>[]]";

            Assert.Equal(288957, task.Solution(navigationSubsystem));
        }
    }
}
