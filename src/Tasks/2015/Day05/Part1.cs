/*
--- Day 5: Doesn't He Have Intern-Elves For This? ---

Santa needs help figuring out which strings in his text file are naughty or
nice.

A nice string is one with all of the following properties:

It contains at least three vowels (aeiou only), like aei, xazegov, or
aeiouaeiouaeiou. It contains at least one letter that appears twice in a row,
like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd). It does not contain the
strings ab, cd, pq, or xy, even if they are part of one of the other
requirements. For example:

- ugknbfddgicrmopn is nice because it has at least three vowels (u...i...o...),
  a double letter (...dd...), and none of the disallowed substrings.
- aaa is nice because it has at least three vowels and a double letter, even
  though the letters used by different rules overlap.
- jchzalrnumimnmhp is naughty because it has no double letter.
- haegwjzuvuyypxyu is naughty because it contains the string xy.
- dvszwmarrgswjxmb is naughty because it contains only one vowel.

How many strings are nice?
*/

using System.Linq;

namespace App.Tasks.Year2015.Day5
{
    public class Part1 : ITask<int>
    {
        private static readonly char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        private static readonly string[] forbiddenSubstrings = { "ab", "cd", "pq", "xy" };

        public int Solution(string input)
        {
            int niceStrings = 0;

            var strings = StringsRepository.GetStrings(input);

            foreach (var str in strings)
            {
                bool hasThreeVowels = HasThreeVowels(str);
                if (hasThreeVowels)
                {
                    bool atLeastOneLetterAppearsConsecutively = AtLeastOneLetterAppearsConsecutively(str);
                    if (atLeastOneLetterAppearsConsecutively)
                    {
                        bool hasForbiddenSubstrings = HasForbiddenSubstrings(str);
                        if (!hasForbiddenSubstrings)
                        {
                            niceStrings++;
                        }
                    }
                }
            }

            return niceStrings;
        }

        private bool HasThreeVowels(string str)
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

        private bool HasForbiddenSubstrings(string str)
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
    }
}
