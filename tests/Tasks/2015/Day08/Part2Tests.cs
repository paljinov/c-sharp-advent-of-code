using App.Tasks.Year2015.Day8;
using Xunit;

namespace Tests.Tasks.Year2015.Day8
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstCodeCharactersExample_EncodedStringMinusOriginalStringLiteralsEquals()
        {
            Assert.Equal(4, task.Solution("\"\""));
        }

        [Fact]
        public void Solution_SecondCodeCharactersExample_EncodedStringMinusOriginalStringLiteralsEquals()
        {
            Assert.Equal(4, task.Solution("\"abc\""));
        }

        [Fact]
        public void Solution_ThirdCodeCharactersExample_EncodedStringMinusOriginalStringLiteralsEquals()
        {
            Assert.Equal(6, task.Solution("\"aaa\\\"aaa\""));
        }

        [Fact]
        public void Solution_FourthCodeCharactersExample_EncodedStringMinusOriginalStringLiteralsEquals()
        {
            Assert.Equal(5, task.Solution("\"\\x27\""));
        }
    }
}
