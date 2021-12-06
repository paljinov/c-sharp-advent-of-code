using App.Tasks.Year2021.Day6;
using Xunit;

namespace Tests.Tasks.Year2021.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_LanternfishInternalTimersExample_LanternfishCountAfterGivenDaysEquals()
        {
            string lanternfishInternalTimers = "3,4,3,1,2";
            Assert.Equal(5934, task.Solution(lanternfishInternalTimers));
        }
    }
}
