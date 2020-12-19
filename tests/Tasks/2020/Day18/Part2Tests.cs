using App.Tasks.Year2020.Day18;
using Xunit;

namespace Tests.Tasks.Year2020.Day18
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstExpressionExample_ResultingValuesSumForAdditionBeforeMultiplicationEquals()
        {
            string expression = "1 + 2 * 3 + 4 * 5 + 6";
            Assert.Equal(231, task.Solution(expression));
        }

        [Fact]
        public void Solution_SecondExpressionExample_ResultingValuesSumForAdditionBeforeMultiplicationEquals()
        {
            string expression = "1 + (2 * 3) + (4 * (5 + 6))";
            Assert.Equal(51, task.Solution(expression));
        }

        [Fact]
        public void Solution_ThirdExpressionExample_ResultingValuesSumForAdditionBeforeMultiplicationEquals()
        {
            string expression = "2 * 3 + (4 * 5)";
            Assert.Equal(46, task.Solution(expression));
        }

        [Fact]
        public void Solution_FourthExpressionExample_ResultingValuesSumForAdditionBeforeMultiplicationEquals()
        {
            string expression = "5 + (8 * 3 + 9 + 3 * 4 * 3)";
            Assert.Equal(1445, task.Solution(expression));
        }

        [Fact]
        public void Solution_FifthExpressionExample_ResultingValuesSumForAdditionBeforeMultiplicationEquals()
        {
            string expression = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
            Assert.Equal(669060, task.Solution(expression));
        }

        [Fact]
        public void Solution_SixthExpressionExample_ResultingValuesSumForAdditionBeforeMultiplicationEquals()
        {
            string expression = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
            Assert.Equal(23340, task.Solution(expression));
        }
    }
}
