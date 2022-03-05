using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2018.Day20;
using Xunit;

namespace Tests.Tasks.Year2018.Day20
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(Regex_LargestNumberOfDoorsNeededToPassThroughToReachRoom_TestData))]
        public void Solution_RegexExample_LargestNumberOfDoorsNeededToPassThroughToReachRoomEquals(
            string regex,
            int largestNumberOfDoorsNeededToPassThroughToReachRoom
        )
        {
            Assert.Equal(largestNumberOfDoorsNeededToPassThroughToReachRoom, task.Solution(regex));
        }

        public class Regex_LargestNumberOfDoorsNeededToPassThroughToReachRoom_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "^WNE$",
                    3
                };

                yield return new object[] {
                    "^ENWWW(NEEE|SSE(EE|N))$",
                    10
                };

                yield return new object[] {
                    "^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$",
                    18
                };

                yield return new object[] {
                    "^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$",
                    23
                };

                yield return new object[] {
                    "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$",
                    31
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
