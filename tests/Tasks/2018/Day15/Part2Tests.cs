using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2018.Day15;
using Xunit;

namespace Tests.Tasks.Year2018.Day15
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [ClassData(typeof(CombatDescription_CombatOutcomeOfTheBattleWithoutAnyElvesDying_TestData))]
        public void Solution_CombatDescriptionExample_CombatOutcomeOfTheBattleWithoutAnyElvesDyingEquals(
            string combatdescription,
            int combatOutcomeOfTheBattleWithoutAnyElvesDying
        )
        {
            Assert.Equal(combatOutcomeOfTheBattleWithoutAnyElvesDying, task.Solution(combatdescription));
        }

        public class CombatDescription_CombatOutcomeOfTheBattleWithoutAnyElvesDying_TestData : IEnumerable<object[]>
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
                    4988
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
                   31284
               };

                yield return new object[] {
                    "#######"
                    + $"{Environment.NewLine}#E.G#.#"
                    + $"{Environment.NewLine}#.#G..#"
                    + $"{Environment.NewLine}#G.#.G#"
                    + $"{Environment.NewLine}#G..#.#"
                    + $"{Environment.NewLine}#...E.#"
                    + $"{Environment.NewLine}#######",
                    3478
                };

                yield return new object[] {
                    "#######"
                    + $"{Environment.NewLine}#.E...#"
                    + $"{Environment.NewLine}#.#..G#"
                    + $"{Environment.NewLine}#.###.#"
                    + $"{Environment.NewLine}#E#G#G#"
                    + $"{Environment.NewLine}#...#G#"
                    + $"{Environment.NewLine}#######",
                    6474
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
                    1140
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
