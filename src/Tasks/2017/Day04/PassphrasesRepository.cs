using System;

namespace App.Tasks.Year2017.Day4
{
    public class PassphrasesRepository
    {
        public string[][] GetPassphrases(string input)
        {
            string[] passphrases = input.Split(Environment.NewLine);

            string[][] passphrasesWords = new string[passphrases.Length][];

            for (int i = 0; i < passphrases.Length; i++)
            {
                string[] words = passphrases[i].Split(' ');
                passphrasesWords[i] = words;
            }

            return passphrasesWords;
        }
    }
}
