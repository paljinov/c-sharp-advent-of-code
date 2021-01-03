using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day6
{
    public class Message
    {
        public string GetErrorCorrectedMessageForMostCommonLetter(string[] messages)
        {
            return GetErrorCorrectedMessage(messages, false);
        }

        public string GetErrorCorrectedMessageForLeastCommonLetter(string[] messages)
        {
            return GetErrorCorrectedMessage(messages, true);
        }

        private string GetErrorCorrectedMessage(string[] messages, bool leastCommonLetter)
        {
            // All messages have the same lenght
            int messageLength = messages[0].Length;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < messageLength; i++)
            {
                Dictionary<char, int> mostFrequentCharacterForPosition = new Dictionary<char, int>();
                foreach (string message in messages)
                {
                    char letter = message[i];

                    if (mostFrequentCharacterForPosition.ContainsKey(letter))
                    {
                        mostFrequentCharacterForPosition[letter]++;
                    }
                    else
                    {
                        mostFrequentCharacterForPosition[letter] = 1;
                    }
                }

                int repetitionCode;
                // Least common letter
                if (leastCommonLetter)
                {
                    repetitionCode = mostFrequentCharacterForPosition.Values.Min();
                }
                // Most common letter
                else
                {
                    repetitionCode = mostFrequentCharacterForPosition.Values.Max();
                }

                char reconstructedLetter = mostFrequentCharacterForPosition.FirstOrDefault(
                    occurrences => occurrences.Value == repetitionCode).Key;
                sb.Append(reconstructedLetter);
            }

            string errorCorrectedMessage = sb.ToString();

            return errorCorrectedMessage;
        }
    }
}
