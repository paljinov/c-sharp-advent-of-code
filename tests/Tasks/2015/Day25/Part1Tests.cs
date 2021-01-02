using App.Tasks.Year2015.Day25;
using Xunit;

namespace Tests.Tasks.Year2015.Day25
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_RowAndColumnExample_CodeEquals()
        {
            Assert.Equal(27995004, task.Solution(
                "To continue, please consult the code grid in the manual.  Enter the code at row 6, column 6."
            ));
        }
    }
}
