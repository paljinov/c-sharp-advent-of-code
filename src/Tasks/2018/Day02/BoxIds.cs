using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day2
{
    public class BoxIds
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

        public string GetCommonLettersBetweenTwoCorrectBoxIds(string[] boxIds)
        {
            string commonLettersBetweenTwoCorrectBoxIds = "";

            foreach (string firstBoxId in boxIds)
            {
                foreach (string secondBoxId in boxIds)
                {
                    if (firstBoxId != secondBoxId)
                    {
                        List<int> differentLettersPositions = GetDifferentLettersPositions(firstBoxId, secondBoxId);
                        if (differentLettersPositions.Count == 1)
                        {
                            commonLettersBetweenTwoCorrectBoxIds =
                                firstBoxId.Remove(differentLettersPositions.First(), 1);
                            break;
                        }
                    }
                }

                if (commonLettersBetweenTwoCorrectBoxIds != "")
                {
                    break;
                }
            }

            return commonLettersBetweenTwoCorrectBoxIds;
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

        private List<int> GetDifferentLettersPositions(string word1, string word2)
        {
            List<int> differentLettersPositions = new List<int>();
            for (int i = 0; i < word1.Length; i++)
            {
                if (word1[i] != word2[i])
                {
                    differentLettersPositions.Add(i);
                }
            }

            return differentLettersPositions;
        }
    }
}
