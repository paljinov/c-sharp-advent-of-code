using App.Tasks.Year2016.Day17;
using Xunit;

namespace Tests.Tasks.Year2016.Day17
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [InlineData("ihgpwlah", 370)]
        [InlineData("kglvqrro", 492)]
        [InlineData("ulqzkmiv", 830)]
        public void Solution_PasscodeExample_LengthOfLongestPathToVaultEquals(
            string passcode,
            int lengthOfLongestPathToVault
        )
        {
            Assert.Equal(lengthOfLongestPathToVault, task.Solution(passcode));
        }
    }
}
