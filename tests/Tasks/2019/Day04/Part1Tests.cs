using App.Tasks.Year2019.Day4;
using Xunit;

namespace Tests.Tasks.Year2019.Day4
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("111111-111111", 1)]
        [InlineData("223450-223450", 0)]
        [InlineData("123789-123789", 0)]
        public void Solution_PasswordsRangeExample_DifferentPasswordsWithinRangeWhichMeetCriteriaCountEquals(
            string passwordsRange,
            int differentPasswordsWithinRangeWhichMeetCriteria
        )
        {
            Assert.Equal(differentPasswordsWithinRangeWhichMeetCriteria, task.Solution(passwordsRange));
        }
    }
}
