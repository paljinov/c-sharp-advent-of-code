using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2021.Day18;
using Xunit;

namespace Tests.Tasks.Year2021.Day18
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(SnailfishNumbers_FinalSumMagnitude_TestData))]
        public void Solution_SnailfishNumbersExample_FinalSumMagnitudeEquals(
            string snailfishNumbers,
            int finalSumMagnitude
        )
        {
            Assert.Equal(finalSumMagnitude, task.Solution(snailfishNumbers));
        }

        public class SnailfishNumbers_FinalSumMagnitude_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "[1,2]"
                    + $"{Environment.NewLine}[[1,2],3]"
                    + $"{Environment.NewLine}[9,[8,7]]"
                    + $"{Environment.NewLine}[[1,9],[8,5]]"
                    + $"{Environment.NewLine}[[[[1,2],[3,4]],[[5,6],[7,8]]],9]"
                    + $"{Environment.NewLine}[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]"
                    + $"{Environment.NewLine}[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]",
                    4230
                };

                yield return new object[] {
                    "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]"
                    + $"{Environment.NewLine}[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]"
                    + $"{Environment.NewLine}[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]"
                    + $"{Environment.NewLine}[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]"
                    + $"{Environment.NewLine}[7,[5,[[3,8],[1,4]]]]"
                    + $"{Environment.NewLine}[[2,[2,2]],[8,[8,1]]]"
                    + $"{Environment.NewLine}[2,9]"
                    + $"{Environment.NewLine}[1,[[[9,3],9],[[9,0],[0,7]]]]"
                    + $"{Environment.NewLine}[[[5,[7,4]],7],1]"
                    + $"{Environment.NewLine}[[[[4,2],2],6],[8,7]]",
                    3488
                };

                yield return new object[] {
                    "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]"
                    + $"{Environment.NewLine}[[[5,[2,8]],4],[5,[[9,9],0]]]"
                    + $"{Environment.NewLine}[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]"
                    + $"{Environment.NewLine}[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]"
                    + $"{Environment.NewLine}[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]"
                    + $"{Environment.NewLine}[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]"
                    + $"{Environment.NewLine}[[[[5,4],[7,7]],8],[[8,3],8]]"
                    + $"{Environment.NewLine}[[9,3],[[9,9],[6,[4,9]]]]"
                    + $"{Environment.NewLine}[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]"
                    + $"{Environment.NewLine}[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]",
                    4140
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
