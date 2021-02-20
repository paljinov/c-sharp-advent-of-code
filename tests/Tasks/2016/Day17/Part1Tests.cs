using App.Tasks.Year2016.Day17;
using Xunit;

namespace Tests.Tasks.Year2016.Day17
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [InlineData("ihgpwlah", "DDRRRD")]
        [InlineData("kglvqrro", "DDUDRLRRUDRD")]
        [InlineData("ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
        public void Solution_PasscodeExample_ShortestPathToVaultEquals(
            string passcode,
            string shortestPathToVault
        )
        {
            Assert.Equal(shortestPathToVault, task.Solution(passcode));
        }
    }
}
