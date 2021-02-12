using App.Tasks.Year2016.Day14;
using Xunit;

namespace Tests.Tasks.Year2016.Day14
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_SaltExample_IndexWhichProducesSixtyFourthKeyEquals()
        {
            string salt = "abc";
            Assert.Equal(22551, task.Solution(salt));
        }
    }
}
