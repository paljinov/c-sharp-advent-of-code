using System;
using App.Tasks.Year2018.Day6;
using Xunit;

namespace Tests.Tasks.Year2018.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_CoordinatesExample_SizeOfTheLargestNonInfiniteAreaEquals()
        {
            string coordinates = "1, 1"
                + $"{Environment.NewLine}1, 6"
                + $"{Environment.NewLine}8, 3"
                + $"{Environment.NewLine}3, 4"
                + $"{Environment.NewLine}5, 5"
                + $"{Environment.NewLine}8, 9";

            Assert.Equal(17, task.Solution(coordinates));
        }
    }
}
