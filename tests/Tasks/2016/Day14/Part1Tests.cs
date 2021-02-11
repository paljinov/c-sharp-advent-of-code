using App.Tasks.Year2016.Day14;
using Xunit;

namespace Tests.Tasks.Year2016.Day14
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_SaltExample_IndexWhichProducesSixtyFourthKeyEquals()
        {
            string salt = "abc";
            Assert.Equal(22728, task.Solution(salt));
        }
    }
}
