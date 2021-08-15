using System;
using App.Tasks.Year2019.Day12;
using Xunit;

namespace Tests.Tasks.Year2019.Day12
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstMoonsPositionsExample_NumberOfStepsNeededToReachFirstStateThatExactlyMatchesPreviousStateEquals()
        {
            string moonsPositions = "<x=-1, y=0, z=2>"
                + $"{Environment.NewLine}<x=2, y=-10, z=-7>"
                + $"{Environment.NewLine}<x=4, y=-8, z=8>"
                + $"{Environment.NewLine}<x=3, y=5, z=-1>";

            Assert.Equal(2772, task.Solution(moonsPositions));
        }

        [Fact]
        public void Solution_SecondMoonsPositionsExample_NumberOfStepsNeededToReachFirstStateThatExactlyMatchesPreviousStateEquals()
        {
            string moonsPositions = "<x=-8, y=-10, z=0>"
                + $"{Environment.NewLine}<x=5, y=5, z=10>"
                + $"{Environment.NewLine}<x=2, y=-7, z=3>"
                + $"{Environment.NewLine}<x=9, y=-8, z=-3>";

            Assert.Equal(4686774924, task.Solution(moonsPositions));
        }
    }
}
