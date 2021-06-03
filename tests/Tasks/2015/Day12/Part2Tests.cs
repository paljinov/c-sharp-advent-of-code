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

        [Theory]
        [InlineData("[1,2,3]", 6)]
        [InlineData("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
        [InlineData("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0)]
        [InlineData("[1,\"red\",5]", 6)]
        public void Solution_JsonDocumentExample_AllNumbersSumWhenIgnoringObjectPropertiesWithValueRedEquals(
            string jsonDocument,
            int sum
        )
        {
            Assert.Equal(sum, task.Solution(jsonDocument));
        }
    }
}
