using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2019.Day18;
using Xunit;

namespace Tests.Tasks.Year2019.Day18
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(TunnelsMap_StepsOfShortestPathThatCollectsAllOfTheKeysCount_TestData))]
        public void Solution_TunnelsMapExample_StepsOfShortestPathThatCollectsAllOfTheKeysCountEquals(
            string tunnelsMap,
            int stepsOfShortestPathThatCollectsAllOfTheKeys
        )
        {
            Assert.Equal(stepsOfShortestPathThatCollectsAllOfTheKeys, task.Solution(tunnelsMap));
        }

        public class TunnelsMap_StepsOfShortestPathThatCollectsAllOfTheKeysCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "#########"
                    + $"{Environment.NewLine}#b.A.@.a#"
                    + $"{Environment.NewLine}#########",
                    8
                };

                yield return new object[] {
                    "########################"
                    + $"{Environment.NewLine}#f.D.E.e.C.b.A.@.a.B.c.#"
                    + $"{Environment.NewLine}######################.#"
                    + $"{Environment.NewLine}#d.....................#"
                    + $"{Environment.NewLine}########################",
                    86
                };

                yield return new object[] {
                    "########################"
                    + $"{Environment.NewLine}#...............b.C.D.f#"
                    + $"{Environment.NewLine}#.######################"
                    + $"{Environment.NewLine}#.....@.a.B.c.d.A.e.F.g#"
                    + $"{Environment.NewLine}########################",
                    132
                };

                yield return new object[] {
                    "#################"
                    + $"{Environment.NewLine}#i.G..c...e..H.p#"
                    + $"{Environment.NewLine}########.########"
                    + $"{Environment.NewLine}#j.A..b...f..D.o#"
                    + $"{Environment.NewLine}########@########"
                    + $"{Environment.NewLine}#k.E..a...g..B.n#"
                    + $"{Environment.NewLine}########.########"
                    + $"{Environment.NewLine}#l.F..d...h..C.m#"
                    + $"{Environment.NewLine}#################",
                    136
                };

                yield return new object[] {
                    "########################"
                    + $"{Environment.NewLine}#@..............ac.GI.b#"
                    + $"{Environment.NewLine}###d#e#f################"
                    + $"{Environment.NewLine}###A#B#C################"
                    + $"{Environment.NewLine}###g#h#i################"
                    + $"{Environment.NewLine}########################",
                    81
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
