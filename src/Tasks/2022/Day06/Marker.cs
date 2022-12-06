using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2022.Day6
{
    public class Marker
    {
        public int CountProcessedCharactersBeforeTheFirstStartOfMarkerIsDetected(
            string datastreamBuffer,
            int markerDistinctCharacters
        )
        {
            int processedCharactersBeforeTheFirstStartOfMarkerIsDetected = 0;

            Queue<char> mostRecentCharacters = new Queue<char>();

            for (int i = 0; i < datastreamBuffer.Length; i++)
            {
                mostRecentCharacters.Enqueue(datastreamBuffer[i]);
                if (mostRecentCharacters.Count == markerDistinctCharacters)
                {
                    if (AreAllCharactersDifferent(mostRecentCharacters))
                    {
                        processedCharactersBeforeTheFirstStartOfMarkerIsDetected = i + 1;
                        break;
                    }

                    mostRecentCharacters.Dequeue();
                }
            }

            return processedCharactersBeforeTheFirstStartOfMarkerIsDetected;
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
