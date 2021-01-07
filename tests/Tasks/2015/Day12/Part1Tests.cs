using App.Tasks.Year2015.Day12;
using Xunit;

namespace Tests.Tasks.Year2015.Day12
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(6, task.Solution("[1,2,3]"));
        }

        [Fact]
        public void Solution_SecondJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(6, task.Solution("{\"a\":2,\"b\":4}"));
        }

        [Fact]
        public void Solution_ThirdJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(3, task.Solution("[[[3]]]"));
        }

        [Fact]
        public void Solution_FourthJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(3, task.Solution("{\"a\":{\"b\":4},\"c\":-1}"));
        }

        [Fact]
        public void Solution_FifthJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(0, task.Solution("{\"a\":[-1,1]}"));
        }

        [Fact]
        public void Solution_SixthJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(0, task.Solution("[-1,{\"a\":1}]"));
        }

        [Fact]
        public void Solution_SeventhJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(0, task.Solution("[]"));
        }

        [Fact]
        public void Solution_EighthJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(0, task.Solution("{}"));
        }
    }
}
