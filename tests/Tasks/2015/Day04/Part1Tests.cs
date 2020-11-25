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

        [Fact]
        public void Solution_FirstExampleSecretKey_CorrectAnswer()
        {
            Assert.Equal(609043, task.Solution("abcdef"));
        }

        [Fact]
        public void Solution_SecondExampleSecretKey_CorrectAnswer()
        {
            Assert.Equal(1048970, task.Solution("pqrstuv"));
        }
    }
}
