/*
--- Part Two ---

Your device's communication system is correctly detecting packets, but still
isn't working. It looks like it also needs to look for messages.

A start-of-message marker is just like a start-of-packet marker, except it
consists of 14 distinct characters rather than 4.

Here are the first positions of start-of-message markers for all of the above
examples:

- mjqjpqmgbljsphdztnvjfqwrcgsmlb: first marker after character 19
- bvwbjplbgvbhsrlpgdmjqwftvncz: first marker after character 23
- nppdvjthqldpwncqszvftbrmjlhg: first marker after character 23
- nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg: first marker after character 29
- zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw: first marker after character 26

How many characters need to be processed before the first start-of-message
marker is detected?
*/

namespace App.Tasks.Year2022.Day6
{
    public class Part2 : ITask<int>
    {
        private const int MARKER_DISTINCT_CHARACTERS = 14;

        private readonly Marker marker;

        public Part2()
        {
            marker = new Marker();
        }

        public int Solution(string input)
        {
            int processedCharactersBeforeTheFirstStartOfMessageMarkerIsDetected =
                marker.CountProcessedCharactersBeforeTheFirstStartOfMarkerIsDetected(input, MARKER_DISTINCT_CHARACTERS);

            return processedCharactersBeforeTheFirstStartOfMessageMarkerIsDetected;
        }
    }
}
