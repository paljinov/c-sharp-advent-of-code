using System;
using App.Tasks.Year2016.Day3;
using Xunit;

namespace Tests.Tasks.Year2016.Day3
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_DesignDocumentExample_PossibleTrianglesEquals()
        {
            string designDocument = "101 301 501"
                + $"{Environment.NewLine}102 302 502"
                + $"{Environment.NewLine}103 303 503"
                + $"{Environment.NewLine}201 401 601"
                + $"{Environment.NewLine}202 402 602"
                + $"{Environment.NewLine}203 403 603";

            Assert.Equal(6, task.Solution(designDocument));
        }
    }
}
