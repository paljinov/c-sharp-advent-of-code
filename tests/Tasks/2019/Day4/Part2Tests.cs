using App.Tasks.Year2019.Day4;
using Xunit;

namespace Tests.Tasks.Year2019.Day4
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("112233-112233", 1)]
        [InlineData("123444-123444", 0)]
        [InlineData("111122-111122", 1)]
        public void Solution_PasswordsRangeExample_DifferentPasswordsWithinRangeWhichMeetCriteriaCountEquals(
            string passwordsRange,
            int differentPasswordsWithinRangeWhichMeetCriteria
        )
        {
            Assert.Equal(differentPasswordsWithinRangeWhichMeetCriteria, task.Solution(passwordsRange));
        }
    }
}
