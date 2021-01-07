using App.Tasks.Year2015.Day12;
using Xunit;

namespace Tests.Tasks.Year2015.Day12
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(6, task.Solution("[1,2,3]"));
        }

        [Fact]
        public void Solution_SecondJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(4, task.Solution("[1,{\"c\":\"red\",\"b\":2},3]"));
        }

        [Fact]
        public void Solution_ThirdJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(0, task.Solution("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}"));
        }

        [Fact]
        public void Solution_FourthJsonDocumentExample_AllNumbersSumEquals()
        {
            Assert.Equal(6, task.Solution("[1,\"red\",5]"));
        }
    }
}
