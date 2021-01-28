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

        [Theory]
        [InlineData("[1,2,3]", 6)]
        [InlineData("{\"a\":2,\"b\":4}", 6)]
        [InlineData("[[[3]]]", 3)]
        [InlineData("{\"a\":{\"b\":4},\"c\":-1}", 3)]
        [InlineData("{\"a\":[-1,1]}", 0)]
        [InlineData("[-1,{\"a\":1}]", 0)]
        [InlineData("[]", 0)]
        [InlineData("{}", 0)]
        public void Solution_JsonDocumentExample_AllNumbersSumEquals(string jsonDocument, int sum)
        {
            Assert.Equal(sum, task.Solution(jsonDocument));
        }
    }
}
