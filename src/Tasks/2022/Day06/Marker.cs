using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2022.Day6
{
    public class Marker
    {
        private const int MARKER_LENGTH = 4;

        public int CountProcessedCharactersBeforeTheFirstStartOfPacketMarkerIsDetected(string datastreamBuffer)
        {
            int processedCharactersBeforeTheFirstStartOfPacketMarkerIsDetected = 0;

            Queue<char> mostRecentFourCharacters = new Queue<char>();

            for (int i = 0; i < datastreamBuffer.Length; i++)
            {
                mostRecentFourCharacters.Enqueue(datastreamBuffer[i]);
                if (mostRecentFourCharacters.Count == MARKER_LENGTH)
                {
                    if (AreAllCharactersDifferent(mostRecentFourCharacters))
                    {
                        processedCharactersBeforeTheFirstStartOfPacketMarkerIsDetected = i + 1;
                        break;
                    }

                    mostRecentFourCharacters.Dequeue();
                }
            }

            return processedCharactersBeforeTheFirstStartOfPacketMarkerIsDetected;
        }

        private bool AreAllCharactersDifferent(Queue<char> mostRecentFourCharacters)
        {
            Dictionary<char, int> charactersOccurences = new Dictionary<char, int>();
            foreach (char character in mostRecentFourCharacters)
            {
                if (charactersOccurences.ContainsKey(character))
                {
                    charactersOccurences[character]++;
                }
                else
                {
                    charactersOccurences[character] = 1;
                }
            }

            int maxOccurences = charactersOccurences.Values.Max();
            if (maxOccurences > 1)
            {
                return false;
            }

            return true;
        }
    }
}
