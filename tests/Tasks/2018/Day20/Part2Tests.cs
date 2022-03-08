using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using App.Tasks.Year2018.Day20;
using Xunit;

namespace Tests.Tasks.Year2018.Day20
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
            task.GetType()
                .GetField("minDoors", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 10);
        }

        [Theory]
        [ClassData(typeof(Regex_RoomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors_TestData))]
        public void Solution_RegexExample_RoomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoorsEquals(
            string regex,
            int roomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors
        )
        {
            Assert.Equal(roomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors, task.Solution(regex));
        }

        public class Regex_RoomsThatHaveShortestPathFromCurrentLocationThatPassThroughAtLeastGivenNumberOfDoors_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "^WNE$",
                    0
                };

                yield return new object[] {
                    "^ENWWW(NEEE|SSE(EE|N))$",
                    1
                };

                yield return new object[] {
                    "^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$",
                    13
                };

                yield return new object[] {
                    "^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$",
                    25
                };

                yield return new object[] {
                    "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$",
                    39
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
