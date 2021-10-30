using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2019.Day18;
using Xunit;

namespace Tests.Tasks.Year2019.Day18
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [ClassData(typeof(TunnelsMap_FewestStepsNecessaryToCollectAllOfTheKeysForUpdatedMapCount_TestData))]
        public void Solution_TunnelsMapExample_FewestStepsNecessaryToCollectAllOfTheKeysForUpdatedMapCountEquals(
            string tunnelsMap,
            int fewestStepsNecessaryToCollectAllOfTheKeysForUpdatedMap
        )
        {
            Assert.Equal(fewestStepsNecessaryToCollectAllOfTheKeysForUpdatedMap, task.Solution(tunnelsMap));
        }

        public class TunnelsMap_FewestStepsNecessaryToCollectAllOfTheKeysForUpdatedMapCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "#######"
                    + $"{Environment.NewLine}#a.#Cd#"
                    + $"{Environment.NewLine}##...##"
                    + $"{Environment.NewLine}##.@.##"
                    + $"{Environment.NewLine}##...##"
                    + $"{Environment.NewLine}#cB#Ab#"
                    + $"{Environment.NewLine}#######",
                    8
                };

                yield return new object[] {
                    "###############"
                    + $"{Environment.NewLine}#d.ABC.#.....a#"
                    + $"{Environment.NewLine}######@#@######"
                    + $"{Environment.NewLine}###############"
                    + $"{Environment.NewLine}######@#@######"
                    + $"{Environment.NewLine}#b.....#.....c#"
                    + $"{Environment.NewLine}###############",
                    24
                };

                yield return new object[] {
                    "#############"
                    + $"{Environment.NewLine}#DcBa.#.GhKl#"
                    + $"{Environment.NewLine}#.###@#@#I###"
                    + $"{Environment.NewLine}#e#d#####j#k#"
                    + $"{Environment.NewLine}###C#@#@###J#"
                    + $"{Environment.NewLine}#fEbA.#.FgHi#"
                    + $"{Environment.NewLine}#############",
                    32
                };

                yield return new object[] {
                    "#############"
                    + $"{Environment.NewLine}#g#f.D#..h#l#"
                    + $"{Environment.NewLine}#F###e#E###.#"
                    + $"{Environment.NewLine}#dCba@#@BcIJ#"
                    + $"{Environment.NewLine}#############"
                    + $"{Environment.NewLine}#nK.L@#@G...#"
                    + $"{Environment.NewLine}#M###N#H###.#"
                    + $"{Environment.NewLine}#o#m..#i#jk.#"
                    + $"{Environment.NewLine}#############",
                    72
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
