using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2015.Day5
{
    public class NiceStrings
    {
        public int CountNiceStringsForPart1Rules(string[] strings, char[] vowels, string[] forbiddenSubstrings)
        {
            int niceStrings = 0;

            foreach (var str in strings)
            {
                bool hasThreeVowels = HasThreeVowels(str, vowels);
                if (hasThreeVowels)
                {
                    bool atLeastOneLetterAppearsConsecutively = AtLeastOneLetterAppearsConsecutively(str);
                    if (atLeastOneLetterAppearsConsecutively)
                    {
                        bool hasForbiddenSubstrings = HasForbiddenSubstrings(str, forbiddenSubstrings);
                        if (!hasForbiddenSubstrings)
                        {
                            niceStrings++;
                        }
                    }
                }
            }

            return niceStrings;
        }

        public int CountNiceStringsForPart2Rules(string[] strings)
        {
            int niceStrings = 0;

            foreach (var str in strings)
            {
                bool hasLetterPair = HasLetterPair(str);
                if (hasLetterPair)
                {
                    bool hasLetterWhichRepeatsWithExactlyOneLetterBetweenThem =
                        HasLetterWhichRepeatsWithExactlyOneLetterBetweenThem(str);
                    if (hasLetterWhichRepeatsWithExactlyOneLetterBetweenThem)
                    {
                        niceStrings++;
                    }
                }
            }

            return niceStrings;
        }

        private bool HasThreeVowels(string str, char[] vowels)
        {
            bool hasThreeVowels = false;

            int stringVowels = 0;

            foreach (char c in str)
            {
                if (vowels.Contains(c))
                {
                    stringVowels++;
                }

                if (stringVowels >= 3)
                {
                    hasThreeVowels = true;
                    break;
                }
            }

            return hasThreeVowels;
        }

        private bool AtLeastOneLetterAppearsConsecutively(string str)
        {
            bool atLeastOneLetterAppearsConsecutively = false;

            for (int i = 0; i < str.Length - 1; i++)
            {
                char currentLetter = str[i];
                char nextLetter = str[i + 1];

                if (currentLetter == nextLetter)
                {
                    atLeastOneLetterAppearsConsecutively = true;
                    break;
                }
            }

            return atLeastOneLetterAppearsConsecutively;
        }

        private bool HasForbiddenSubstrings(string str, string[] forbiddenSubstrings)
        {
            bool hasForbiddenSubstrings = false;

            foreach (var forbiddenSubstring in forbiddenSubstrings)
            {
                if (str.Contains(forbiddenSubstring))
                {
                    hasForbiddenSubstrings = true;
                    break;
                }
            }

            return hasForbiddenSubstrings;
        }

        private bool HasLetterPair(string str)
        {
            bool hasLetterPair = false;

            List<string> letterPairs = new List<string>();

            for (int i = 0; i < str.Length - 1; i++)
            {
                string letterPair = new string(new char[] { str[i], str[i + 1] });
                if (letterPairs.Contains(letterPair))
                {
                    hasLetterPair = true;
                    break;
                }

                letterPairs.Add(letterPair);

                // If character overlaps like 'aaa' we will not add it as new pair
                if (str[i] == str[i + 1] && (i + 2 < str.Length && str[i + 1] == str[i + 2]))
                {
                    i++;
                }
            }

            return hasLetterPair;
        }

        private bool HasLetterWhichRepeatsWithExactlyOneLetterBetweenThem(string str)
        {
            bool hasLetterWhichRepeatsWithExactlyOneLetterBetweenThem = false;

            for (int i = 0; i < str.Length - 2; i++)
            {
                char currentLetter = str[i];
                char twoPlacesAheeadLetter = str[i + 2];

                if (currentLetter == twoPlacesAheeadLetter)
                {
                    hasLetterWhichRepeatsWithExactlyOneLetterBetweenThem = true;
                    break;
                }
            }

            return hasLetterWhichRepeatsWithExactlyOneLetterBetweenThem;
        }
    }
}
