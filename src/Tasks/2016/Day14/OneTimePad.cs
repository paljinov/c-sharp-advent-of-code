using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace App.Tasks.Year2016.Day14
{
    public class OneTimePad
    {
        private const int INDEX_STARTS_AT = 0;

        private const int SIXTY_FOURTH_KEY_INDEX = 64;

        private const int SEARCH_QUINTUPLET_HASHES = 1000;

        private readonly Dictionary<string, string> hashCache = new Dictionary<string, string>();

        public int GetIndexWhichProducesSixtyFourthKey(string salt, int additionalHashings = 0)
        {
            int[] keysIndexes = new int[SIXTY_FOURTH_KEY_INDEX];
            int nextIndex = 0;

            int i = INDEX_STARTS_AT;
            while (nextIndex < SIXTY_FOURTH_KEY_INDEX)
            {
                string hash = GenerateHash(salt, i, additionalHashings);

                (bool isTriplet, char? repeatingCharacter) = IsCharacterRepeating(hash, 3);
                if (isTriplet)
                {
                    for (int j = i + 1; j < i + 1 + SEARCH_QUINTUPLET_HASHES; j++)
                    {
                        hash = GenerateHash(salt, j, additionalHashings);

                        (bool isQuintuplet, _) = IsCharacterRepeating(hash, 5, repeatingCharacter.Value);
                        if (isQuintuplet)
                        {
                            keysIndexes[nextIndex] = i;
                            nextIndex++;
                            break;
                        }
                    }
                }

                i++;
            }

            return keysIndexes[^1];
        }

        private string GenerateHash(string salt, int index, int additionalHashings)
        {
            string saltWithIndex = $"{salt}{index}";

            // Fetching hash from cache
            if (hashCache.ContainsKey(saltWithIndex))
            {
                return hashCache[saltWithIndex];
            }

            string hash = GetMd5HashForString($"{salt}{index}");
            for (int i = 0; i < additionalHashings; i++)
            {
                hash = GetMd5HashForString(hash);
            }

            // Storing hash in cache
            hashCache[saltWithIndex] = hash;

            return hash;
        }

        /// <summary>
        /// Check is character repeating in hash asked number of times.
        /// </summary>
        /// <param name="hash">Hash</param>
        /// <param name="repetitions">Minimum number of repetitions</param>
        /// <param name="repeatingCharacter">If null any character can repeat asked number of times,
        /// otherwise exactly this character must repeat asked number of times</param>
        /// <returns></returns>
        private (bool, char?) IsCharacterRepeating(string hash, int repetitions, char? repeatingCharacter = null)
        {
            for (int i = 0; i <= hash.Length - repetitions; i++)
            {
                char c = hash[i];
                // If exact character must repeat
                if (repeatingCharacter.HasValue)
                {
                    c = repeatingCharacter.Value;
                }

                bool repeatsAskedNumberOfTimes = true;
                for (int j = 0; j < repetitions; j++)
                {
                    if (c != hash[i + j])
                    {
                        repeatsAskedNumberOfTimes = false;
                        break;
                    }
                }

                if (repeatsAskedNumberOfTimes)
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
