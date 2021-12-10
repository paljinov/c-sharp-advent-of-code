using System;
using App.Tasks.Year2021.Day10;
using Xunit;

namespace Tests.Tasks.Year2021.Day10
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_NavigationSubsystemExample_TotalErrorsSyntaxScoreEquals()
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

            Assert.Equal(26397, task.Solution(navigationSubsystem));
        }
    }
}
