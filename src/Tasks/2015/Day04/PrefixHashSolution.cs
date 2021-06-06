using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Tasks.Year2015.Day4
{
    public class PrefixHashSolution
    {
        public int FindIntegerWhichGivesMd5HashWithPrefix(string secretKey, string hashStartsWithPrefix)
        {
            int integerWhichGivesMd5HashWithPrefix = 0;

            string hash = string.Empty;
            while (integerWhichGivesMd5HashWithPrefix == 0)
            {
                Parallel.ForEach(Integers(), (i, state) =>
                {
                    hash = GetMd5HashForString(secretKey + i);
                    if (hash.StartsWith(hashStartsWithPrefix))
                    {
                        integerWhichGivesMd5HashWithPrefix = i;
                        state.Stop();
                    }
                });
            }

            return integerWhichGivesMd5HashWithPrefix;
        }

        private string GetMd5HashForString(string input)
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

        private IEnumerable<int> Integers()
        {
            int i = 0;
            while (i <= int.MaxValue)
            {
                yield return i;
                i++;
            }
        }
    }
}
