using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2019.Day3;
using Xunit;

namespace Tests.Tasks.Year2019.Day3
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(WiresPaths_ManhattanDistanceFromTheCentralPortToTheClosestIntersection_TestData))]
        public void Solution_WiresPathsExample_ManhattanDistanceFromTheCentralPortToTheClosestIntersectionEquals(
            string wiresPaths,
            int manhattanDistanceFromTheCentralPortToTheClosestIntersection
        )
        {
            Assert.Equal(manhattanDistanceFromTheCentralPortToTheClosestIntersection, task.Solution(wiresPaths));
        }
    }

    public class WiresPaths_ManhattanDistanceFromTheCentralPortToTheClosestIntersection_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                $"R8,U5,L5,D3{Environment.NewLine}U7,R6,D4,L4",
                6
            };
            yield return new object[] {
                $"R75,D30,R83,U83,L12,D49,R71,U7,L72{Environment.NewLine}U62,R66,U55,R34,D71,R55,D58,R83",
                159
            };
            yield return new object[] {
                $"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51{Environment.NewLine}U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",
                135
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
