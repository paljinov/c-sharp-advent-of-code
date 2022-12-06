using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2022.Day6;
using Xunit;

namespace Tests.Tasks.Year2022.Day6
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [ClassData(typeof(DatastreamBuffer_ProcessedCharactersBeforeTheFirstStartOfMessageMarkerIsDetectedCount_TestData))]
        public void Solution_DatastreamBufferExample_ProcessedCharactersBeforeTheFirstStartOfMessageMarkerIsDetectedCountEquals(
            string datastreamBuffer,
            int processedCharactersBeforeTheFirstStartOfMessageMarkerIsDetected
        )
        {
            Assert.Equal(
                processedCharactersBeforeTheFirstStartOfMessageMarkerIsDetected,
                task.Solution(datastreamBuffer)
            );
        }

        public class DatastreamBuffer_ProcessedCharactersBeforeTheFirstStartOfMessageMarkerIsDetectedCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
                    19
                };

                yield return new object[] {
                    "bvwbjplbgvbhsrlpgdmjqwftvncz",
                    23
                };

                yield return new object[] {
                    "nppdvjthqldpwncqszvftbrmjlhg",
                    23
                };

                yield return new object[] {
                    "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
                    29
                };

                yield return new object[] {
                    "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",
                    26
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
