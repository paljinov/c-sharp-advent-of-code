using App.Tasks.Year2020.Day18;
using Xunit;

namespace Tests.Tasks.Year2020.Day18
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstExpressionExample_ResultingValuesSumForSamePrecedenceEquals()
        {
            string expression = "1 + 2 * 3 + 4 * 5 + 6";
            Assert.Equal(71, task.Solution(expression));
        }

        [Fact]
        public void Solution_SecondExpressionExample_ResultingValuesSumForSamePrecedenceEquals()
        {
            string expression = "1 + (2 * 3) + (4 * (5 + 6))";
            Assert.Equal(51, task.Solution(expression));
        }

        [Fact]
        public void Solution_ThirdExpressionExample_ResultingValuesSumForSamePrecedenceEquals()
        {
            string expression = "2 * 3 + (4 * 5)";
            Assert.Equal(26, task.Solution(expression));
        }

        [Fact]
        public void Solution_FourthExpressionExample_ResultingValuesSumForSamePrecedenceEquals()
        {
            string expression = "5 + (8 * 3 + 9 + 3 * 4 * 3)";
            Assert.Equal(437, task.Solution(expression));
        }

        [Fact]
        public void Solution_FifthExpressionExample_ResultingValuesSumForSamePrecedenceEquals()
        {
            string expression = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
            Assert.Equal(12240, task.Solution(expression));
        }

        [Fact]
        public void Solution_SixthExpressionExample_ResultingValuesSumForSamePrecedenceEquals()
        {
            string expression = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
            Assert.Equal(13632, task.Solution(expression));
        }
    }
}
