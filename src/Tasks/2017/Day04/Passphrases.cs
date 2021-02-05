using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day4
{
    public class Passphrases
    {
        public int CountValidPassphrasesForDuplicateWordsCondition(string[][] passphrases)
        {
            int validPassphrases = 0;

            foreach (string[] passphraseWords in passphrases)
            {
                if (!ContainsDuplicateWords(passphraseWords))
                {
                    validPassphrases++;
                }
            }

            return validPassphrases;
        }

        public int CountValidPassphrasesForAnagramsCondition(string[][] passphrases)
        {
            int validPassphrases = 0;

            foreach (string[] passphraseWords in passphrases)
            {
                if (!ContainsAnagrams(passphraseWords))
                {
                    validPassphrases++;
                }
            }

            return validPassphrases;
        }

        private bool ContainsDuplicateWords(string[] passphraseWords)
        {
            Dictionary<string, int> occurrences = new Dictionary<string, int>();

            foreach (string word in passphraseWords)
            {
                if (occurrences.ContainsKey(word))
                {
                    occurrences[word] += 1;
                }
                else
                {
                    occurrences[word] = 1;
                }
            }

            if (occurrences.Values.Max() > 1)
            {
                return true;
            }

            return false;
        }

        private bool ContainsAnagrams(string[] passphraseWords)
        {
            string[] wordsWithAlphabeticallySortedLetters = new string[passphraseWords.Length];

            for (int i = 0; i < passphraseWords.Length; i++)
            {
                char[] characters = passphraseWords[i].ToArray();
                Array.Sort(characters);

                wordsWithAlphabeticallySortedLetters[i] = new string(characters);
            }

            bool containsAnagrams = ContainsDuplicateWords(wordsWithAlphabeticallySortedLetters);

            return containsAnagrams;
        }
    }
}
