using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2018.Day25;
using Xunit;

namespace Tests.Tasks.Year2018.Day25
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(FixedPointsInSpacetime_FormedConstellationsByTheFixedPointsInSpacetime_TestData))]
        public void Solution_FixedPointsInSpacetimeExample_FormedConstellationsByTheFixedPointsInSpacetimeEquals(
            string fixedPointsInSpacetime,
            int formedConstellationsByTheFixedPointsInSpacetime
        )
        {
            Assert.Equal(formedConstellationsByTheFixedPointsInSpacetime, task.Solution(fixedPointsInSpacetime));
        }

        public class FixedPointsInSpacetime_FormedConstellationsByTheFixedPointsInSpacetime_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "0,0,0,0"
                    + $"{Environment.NewLine}3,0,0,0"
                    + $"{Environment.NewLine}0,3,0,0"
                    + $"{Environment.NewLine}0,0,3,0"
                    + $"{Environment.NewLine}0,0,0,3"
                    + $"{Environment.NewLine}0,0,0,6"
                    + $"{Environment.NewLine}9,0,0,0"
                    + $"{Environment.NewLine}12,0,0,0",
                    2
                };

                yield return new object[] {
                    "-1,2,2,0"
                    + $"{Environment.NewLine}0,0,2,-2"
                    + $"{Environment.NewLine}0,0,0,-2"
                    + $"{Environment.NewLine}-1,2,0,0"
                    + $"{Environment.NewLine}-2,-2,-2,2"
                    + $"{Environment.NewLine}3,0,2,-1"
                    + $"{Environment.NewLine}-1,3,2,2"
                    + $"{Environment.NewLine}-1,0,-1,0"
                    + $"{Environment.NewLine}0,2,1,-2"
                    + $"{Environment.NewLine}3,0,0,0",
                    4
                };

                yield return new object[] {
                    "1,-1,0,1"
                    + $"{Environment.NewLine}2,0,-1,0"
                    + $"{Environment.NewLine}3,2,-1,0"
                    + $"{Environment.NewLine}0,0,3,1"
                    + $"{Environment.NewLine}0,0,-1,-1"
                    + $"{Environment.NewLine}2,3,-2,0"
                    + $"{Environment.NewLine}-2,2,0,0"
                    + $"{Environment.NewLine}2,-2,0,-1"
                    + $"{Environment.NewLine}1,-1,0,-1"
                    + $"{Environment.NewLine}3,2,0,2",
                    3
                };

                yield return new object[] {
                    "1,-1,-1,-2"
                    + $"{Environment.NewLine}-2,-2,0,1"
                    + $"{Environment.NewLine}0,2,1,3"
                    + $"{Environment.NewLine}-2,3,-2,1"
                    + $"{Environment.NewLine}0,2,3,-2"
                    + $"{Environment.NewLine}-1,-1,1,-2"
                    + $"{Environment.NewLine}0,-2,-1,0"
                    + $"{Environment.NewLine}-2,2,3,-1"
                    + $"{Environment.NewLine}1,2,2,0"
                    + $"{Environment.NewLine}-1,-2,0,-2",
                    8
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
