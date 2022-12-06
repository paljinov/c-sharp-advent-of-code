using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2022.Day6;
using Xunit;

namespace Tests.Tasks.Year2022.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(DatastreamBuffer_ProcessedCharactersBeforeTheFirstStartOfPacketMarkerIsDetectedCount_TestData))]
        public void Solution_DatastreamBufferExample_ProcessedCharactersBeforeTheFirstStartOfPacketMarkerIsDetectedCountEquals(
            string datastreamBuffer,
            int processedCharactersBeforeTheFirstStartOfPacketMarkerIsDetected
        )
        {
            Assert.Equal(
                processedCharactersBeforeTheFirstStartOfPacketMarkerIsDetected,
                task.Solution(datastreamBuffer)
            );
        }

        public class DatastreamBuffer_ProcessedCharactersBeforeTheFirstStartOfPacketMarkerIsDetectedCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
                    7
                };

                yield return new object[] {
                    "bvwbjplbgvbhsrlpgdmjqwftvncz",
                    5
                };

                yield return new object[] {
                    "nppdvjthqldpwncqszvftbrmjlhg",
                    6
                };

                yield return new object[] {
                    "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
                    10
                };

                yield return new object[] {
                    "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",
                    11
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
