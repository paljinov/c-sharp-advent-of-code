using App.Tasks.Year2021.Day6;
using Xunit;

namespace Tests.Tasks.Year2021.Day6
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_LanternfishInternalTimersExample_LanternfishCountAfterGivenDaysEquals()
        {
            string lanternfishInternalTimers = "3,4,3,1,2";
            Assert.Equal(26984457539, task.Solution(lanternfishInternalTimers));
        }
    }
}
