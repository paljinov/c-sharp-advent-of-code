using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2021.Day12;
using Xunit;

namespace Tests.Tasks.Year2021.Day12
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(CaveSystem_CaveSystemPathsThatVisitSmallCavesAtMostOnceCount_TestData))]
        public void Solution_CaveSystemExample_CaveSystemPathsThatVisitSmallCavesAtMostOnceCountEquals(
            string caveSystem,
            int caveSystemPathsThatVisitSmallCavesAtMostOnce
        )
        {
            Assert.Equal(caveSystemPathsThatVisitSmallCavesAtMostOnce, task.Solution(caveSystem));
        }

        public class CaveSystem_CaveSystemPathsThatVisitSmallCavesAtMostOnceCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "start-A"
                    + $"{Environment.NewLine}start-b"
                    + $"{Environment.NewLine}A-c"
                    + $"{Environment.NewLine}A-b"
                    + $"{Environment.NewLine}b-d"
                    + $"{Environment.NewLine}A-end"
                    + $"{Environment.NewLine}b-end",
                    10
                };

                yield return new object[] {
                    "dc-end"
                    + $"{Environment.NewLine}HN-start"
                    + $"{Environment.NewLine}start-kj"
                    + $"{Environment.NewLine}dc-start"
                    + $"{Environment.NewLine}dc-HN"
                    + $"{Environment.NewLine}LN-dc"
                    + $"{Environment.NewLine}HN-end"
                    + $"{Environment.NewLine}kj-sa"
                    + $"{Environment.NewLine}kj-HN"
                    + $"{Environment.NewLine}kj-dc",
                    19
                };

                yield return new object[] {
                    "fs-end"
                    + $"{Environment.NewLine}he-DX"
                    + $"{Environment.NewLine}fs-he"
                    + $"{Environment.NewLine}start-DX"
                    + $"{Environment.NewLine}pj-DX"
                    + $"{Environment.NewLine}end-zg"
                    + $"{Environment.NewLine}zg-sl"
                    + $"{Environment.NewLine}zg-pj"
                    + $"{Environment.NewLine}pj-he"
                    + $"{Environment.NewLine}RW-he"
                    + $"{Environment.NewLine}fs-DX"
                    + $"{Environment.NewLine}pj-RW"
                    + $"{Environment.NewLine}zg-RW"
                    + $"{Environment.NewLine}start-pj"
                    + $"{Environment.NewLine}he-WI"
                    + $"{Environment.NewLine}zg-he"
                    + $"{Environment.NewLine}pj-fs"
                    + $"{Environment.NewLine}start-RW",
                    226
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
