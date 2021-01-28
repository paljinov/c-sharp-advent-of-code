using App.Tasks.Year2015.Day4;
using Xunit;

namespace Tests.Tasks.Year2015.Day4
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("abcdef", 609043)]
        [InlineData("pqrstuv", 1048970)]
        public void Solution_SecretKeyExample_IntegerWhichGivesMd5HashWithFiveZerosPrefixEquals(
            string secretKey,
            int integer
        )
        {
            Assert.Equal(integer, task.Solution(secretKey));
        }
    }
}
