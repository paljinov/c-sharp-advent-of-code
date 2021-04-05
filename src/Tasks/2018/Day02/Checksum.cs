using System.Collections.Generic;

namespace App.Tasks.Year2018.Day2
{
    public class Checksum
    {
        public int CalculateBoxIdsChecksum(string[] boxIds)
        {
            int exactlyTwoOfAnyLetter = 0;
            int exactlyThreeOfAnyLetter = 0;

            foreach (string boxId in boxIds)
            {
                Dictionary<char, int> letters = CountLetters(boxId);
                if (letters.ContainsValue(2))
                {
                    exactlyTwoOfAnyLetter++;
                }
                if (letters.ContainsValue(3))
                {
                    exactlyThreeOfAnyLetter++;
                }
            }

            int boxIdsChecksum = exactlyTwoOfAnyLetter * exactlyThreeOfAnyLetter;
            return boxIdsChecksum;
        }

        private Dictionary<char, int> CountLetters(string word)
        {
            Dictionary<char, int> letters = new Dictionary<char, int>();
            foreach (char c in word)
            {
                if (letters.ContainsKey(c))
                {
                    letters[c]++;
                }
                else
                {
                    letters[c] = 1;
                }
            }

            return letters;
        }
    }
}
