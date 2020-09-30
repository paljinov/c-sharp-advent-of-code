using System.Security.Cryptography;
using System.Text;

namespace App.Tasks.Year2015.Day4
{
    class PrefixHashSolution
    {
        public static int FindIntegerWhichGivesMd5HashWithPrefix(string secretKey, string startsWithPrefix)
        {
            string hash = string.Empty;
            int i = 0;

            while (!hash.StartsWith(startsWithPrefix))
            {
                i++;
                hash = GetMd5HashForString(secretKey + i);
            }

            return i;
        }

        public static string GetMd5HashForString(string input)
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
