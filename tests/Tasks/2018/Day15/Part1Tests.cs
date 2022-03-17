using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2018.Day15;
using Xunit;

namespace Tests.Tasks.Year2018.Day15
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(CombatDescription_CombatOutcome_TestData))]
        public void Solution_CombatDescriptionExample_CombatOutcomeEquals(string combatdescription, int combatOutcome)
        {
            Assert.Equal(combatOutcome, task.Solution(combatdescription));
        }

        public class CombatDescription_CombatOutcome_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "#######"
                    + $"{Environment.NewLine}#.G...#"
                    + $"{Environment.NewLine}#...EG#"
                    + $"{Environment.NewLine}#.#.#G#"
                    + $"{Environment.NewLine}#..G#E#"
                    + $"{Environment.NewLine}#.....#"
                    + $"{Environment.NewLine}#######",
                    27730
                };

                yield return new object[] {
                    "#######"
                    + $"{Environment.NewLine}#G..#E#"
                    + $"{Environment.NewLine}#E#E.E#"
                    + $"{Environment.NewLine}#G.##.#"
                    + $"{Environment.NewLine}#...#E#"
                    + $"{Environment.NewLine}#...E.#"
                    + $"{Environment.NewLine}#######",
                    36334
                };

                yield return new object[] {
                   "#######"
                    + $"{Environment.NewLine}#E..EG#"
                    + $"{Environment.NewLine}#.#G.E#"
                    + $"{Environment.NewLine}#E.##E#"
                    + $"{Environment.NewLine}#G..#.#"
                    + $"{Environment.NewLine}#..E#.#"
                    + $"{Environment.NewLine}#######",
                   39514
               };

                yield return new object[] {
                    "#######"
                    + $"{Environment.NewLine}#E.G#.#"
                    + $"{Environment.NewLine}#.#G..#"
                    + $"{Environment.NewLine}#G.#.G#"
                    + $"{Environment.NewLine}#G..#.#"
                    + $"{Environment.NewLine}#...E.#"
                    + $"{Environment.NewLine}#######",
                    27755
                };

                yield return new object[] {
                    "#######"
                    + $"{Environment.NewLine}#.E...#"
                    + $"{Environment.NewLine}#.#..G#"
                    + $"{Environment.NewLine}#.###.#"
                    + $"{Environment.NewLine}#E#G#G#"
                    + $"{Environment.NewLine}#...#G#"
                    + $"{Environment.NewLine}#######",
                    28944
                };

                yield return new object[] {
                    "#########"
                    + $"{Environment.NewLine}#G......#"
                    + $"{Environment.NewLine}#.E.#...#"
                    + $"{Environment.NewLine}#..##..G#"
                    + $"{Environment.NewLine}#...##..#"
                    + $"{Environment.NewLine}#...#...#"
                    + $"{Environment.NewLine}#.G...G.#"
                    + $"{Environment.NewLine}#.....G.#"
                    + $"{Environment.NewLine}#########",
                    18740
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
