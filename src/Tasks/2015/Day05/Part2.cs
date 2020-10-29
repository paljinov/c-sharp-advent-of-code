/*
--- Part Two ---

Realizing the error of his ways, Santa has switched to a better model of
determining whether a string is naughty or nice. None of the old rules apply, as
they are all clearly ridiculous.

Now, a nice string is one with all of the following properties:

It contains a pair of any two letters that appears at least twice in the string
without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa,
but it overlaps). It contains at least one letter which repeats with exactly one
letter between them, like xyx, abcdefeghi (efe), or even aaa. For example:

- qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice (qj) and a
  letter that repeats with exactly one letter between them (zxz).
- xxyxx is nice because it has a pair that appears twice and a letter that
  repeats with one between, even though the letters used by each rule overlap.
- uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat with a
  single letter between them.
- ieodomkazucvgmuy is naughty because it has a repeating letter with one between
  (odo), but no pair that appears twice.

How many strings are nice under these new rules?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day5
{
    class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            int niceStrings = 0;

            var strings = StringsRepository.GetStrings(input);

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
