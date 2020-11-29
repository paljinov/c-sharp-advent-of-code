using App.Tasks.Year2015.Day8;
using Xunit;

namespace Tests.Tasks.Year2015.Day8
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstCodeCharactersExample_StringLiteralsMinusStringMemoryEquals()
        {
            Assert.Equal(2, task.Solution("\"\""));
        }

        [Fact]
        public void Solution_SecondCodeCharactersExample_StringLiteralsMinusStringMemoryEquals()
        {
            Assert.Equal(2, task.Solution("\"abc\""));
        }

        [Fact]
        public void Solution_ThirdCodeCharactersExample_StringLiteralsMinusStringMemoryEquals()
        {
            Assert.Equal(3, task.Solution("\"aaa\\\"aaa\""));
        }

        [Fact]
        public void Solution_FourthCodeCharactersExample_StringLiteralsMinusStringMemoryEquals()
        {
            Assert.Equal(5, task.Solution("\"\\x27\""));
        }
    }
}
