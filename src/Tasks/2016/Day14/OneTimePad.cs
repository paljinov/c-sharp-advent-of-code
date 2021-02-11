using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace App.Tasks.Year2016.Day14
{
    public class OneTimePad
    {
        private const int INDEX_STARTS_AT = 0;

        private const int KEY_INDEX = 64;

        private const int SEARCH_QUINTUPLET_HASHES = 1000;

        public int GetIndexWhichProducesSixtyFourthKey(string salt)
        {
            Dictionary<int, string> keys = new Dictionary<int, string>();

            int i = INDEX_STARTS_AT;
            while (keys.Count < KEY_INDEX)
            {
                string hash = GetMd5HashForString($"{salt}{i}");

                (bool isTriplet, char? repeatingCharacter) = CharacterRepeatsWantedNumberOfTimes(hash, 3);
                if (isTriplet)
                {
                    for (int j = i + 1; j < i + 1 + SEARCH_QUINTUPLET_HASHES; j++)
                    {
                        string potentialQuintupletHash = GetMd5HashForString($"{salt}{j}");

                        (bool isQuintuplet, _) =
                            CharacterRepeatsWantedNumberOfTimes(potentialQuintupletHash, 5, repeatingCharacter.Value);

                        if (isQuintuplet)
                        {
                            keys.Add(i, hash);
                            break;
                        }
                    }
                }

                i++;
            }

            return keys.Keys.Last();
        }

        private (bool, char?) CharacterRepeatsWantedNumberOfTimes(
            string hash,
            int repetitions,
            char? repeatingCharacter = null
        )
        {
            for (int i = 0; i <= hash.Length - repetitions; i++)
            {
                char c = hash[i];
                if (repeatingCharacter.HasValue)
                {
                    c = repeatingCharacter.Value;
                }

                bool repeatsWantedNumberOfTimes = true;
                for (int j = 0; j < repetitions; j++)
                {
                    if (c != hash[i + j])
                    {
                        repeatsWantedNumberOfTimes = false;
                        break;
                    }
                }

                if (repeatsWantedNumberOfTimes)
                {
                    return (true, c);
                }
            }

            return (false, null);
        }

        public string GetMd5HashForString(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            string hash = sb.ToString();

            return hash;
        }
    }
}
